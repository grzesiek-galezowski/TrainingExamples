using System;
using Autofac;
using Autofac.Core;
using Autofac.Features.AttributeFilters;
using NUnit.Framework;

//bug object encapsulation (check if this is possible in a container)
//it is to some extent by using child scopes(?) but that makes these places dependent on the container

namespace IoCContainerCons
{
  /// <summary>
  /// "Note that some relationships are based on types that are in Autofac.
  /// Using those relationship types do tie you to at least having a reference to Autofac, even
  /// if you choose to use a different IoC container for the actual resolution of services." 
  /// </summary>
  public class TwoImplementationsOfTheSameInterface
  {
    [Test]
    public void HandMadeComposition()
    {
      var archiveService = new ArchiveService(
        new LocalDataStorage(),
        new RemoteDataStorage());

      Console.WriteLine(archiveService);
    }

    [Test]
    //also: keyed (below) and indexed (and named parameters for constants)
    public void ContainerCompositionWithNamed()
    {
      var containerBuilder = new ContainerBuilder();
      containerBuilder.RegisterType<LocalDataStorage>().Named<IDataStorage>("local");
      containerBuilder.RegisterType<RemoteDataStorage>().Named<IDataStorage>("remote");
      containerBuilder.Register(c =>
        new ArchiveService(
          c.ResolveNamed<IDataStorage>("local"),
          c.ResolveNamed<IDataStorage>("remote")));

      using var container = containerBuilder.Build();
      var archiveService = container.Resolve<ArchiveService>();
      Assert.IsInstanceOf<LocalDataStorage>(archiveService.LocalStorage);
      Assert.IsInstanceOf<RemoteDataStorage>(archiveService.RemoteStorage);
    }

    [Test]
    public void ContainerCompositionThroughResolveKeyedComponents()
    {
      var containerBuilder = new ContainerBuilder();
      containerBuilder.RegisterType<LocalDataStorage>().Keyed<IDataStorage>(Storages.Local);
      containerBuilder.RegisterType<RemoteDataStorage>().Keyed<IDataStorage>(Storages.Remote);
      containerBuilder.RegisterType<ArchiveService>();

      using var container = containerBuilder.Build();
      var archiveService = container.Resolve<ArchiveService>(
        new ResolvedParameter(
          (info, context) => info.Name == "localStorage",
          (info, context) => context.ResolveKeyed<IDataStorage>(Storages.Local)),
        new ResolvedParameter(
          (info, context) => info.Name == "remoteStorage",
          (info, context) => context.ResolveKeyed<IDataStorage>(Storages.Remote))
      );
      Assert.IsInstanceOf<LocalDataStorage>(archiveService.LocalStorage);
      Assert.IsInstanceOf<RemoteDataStorage>(archiveService.RemoteStorage);
    }

    [Test]
    public void ContainerCompositionThroughRegistrationOfResolvedParameters()
    {
      var containerBuilder = new ContainerBuilder();
      containerBuilder.RegisterType<LocalDataStorage>().Keyed<IDataStorage>(Storages.Local);
      containerBuilder.RegisterType<RemoteDataStorage>().Keyed<IDataStorage>(Storages.Remote);
      containerBuilder.RegisterType<ArchiveService>()
        .WithParameter(new ResolvedParameter(
          (info, context) => info.Name == "localStorage",
          (info, context) => context.ResolveKeyed<IDataStorage>(Storages.Local)))
        .WithParameter(new ResolvedParameter(
          (info, context) => info.Name == "remoteStorage",
          (info, context) => context.ResolveKeyed<IDataStorage>(Storages.Remote)));

      using var container = containerBuilder.Build();
      var archiveService = container.Resolve<ArchiveService>();
      Assert.IsInstanceOf<LocalDataStorage>(archiveService.LocalStorage);
      Assert.IsInstanceOf<RemoteDataStorage>(archiveService.RemoteStorage);
    }

    [Test]
    public void ContainerCompositionWithAttributes()
    {
      var containerBuilder = new ContainerBuilder();
      containerBuilder.RegisterType<LocalDataStorage>().Keyed<IDataStorage>(Storages.Local);
      containerBuilder.RegisterType<RemoteDataStorage>().Keyed<IDataStorage>(Storages.Remote);
      containerBuilder.RegisterType<ArchiveServiceAttributed>().WithAttributeFiltering();

      using var container = containerBuilder.Build();
      var archiveService = container.Resolve<ArchiveServiceAttributed>();
      Assert.IsInstanceOf<LocalDataStorage>(archiveService.LocalStorage);
      Assert.IsInstanceOf<RemoteDataStorage>(archiveService.RemoteStorage);
    }

    [Test]
    public void ContainerCompositionThroughNamedParameters()
    {
      //only for up-front known values and constants - cannot resolve from container each time
      //prone to name change(?)
      var containerBuilder = new ContainerBuilder();
      containerBuilder.RegisterType<ArchiveService>()
        .WithParameter("localStorage", new LocalDataStorage())
        .WithParameter("remoteStorage", new RemoteDataStorage());

      using var container = containerBuilder.Build();
      var archiveService = container.Resolve<ArchiveService>();

      Assert.IsInstanceOf<LocalDataStorage>(archiveService.LocalStorage);
      Assert.IsInstanceOf<RemoteDataStorage>(archiveService.RemoteStorage);
    }


    public class LocalDataStorage : IDataStorage
    {
    }

    public class ArchiveService
    {
      public IDataStorage LocalStorage { get; }
      public IDataStorage RemoteStorage { get; }

      public ArchiveService(IDataStorage localStorage, IDataStorage remoteStorage)
      {
        LocalStorage = localStorage;
        RemoteStorage = remoteStorage;
      }
    }

    public class ArchiveServiceAttributed
    {
      public IDataStorage LocalStorage { get; }
      public IDataStorage RemoteStorage { get; }

      public ArchiveServiceAttributed(
        [KeyFilter(Storages.Local)] IDataStorage localStorage,
        [KeyFilter(Storages.Remote)] IDataStorage remoteStorage)
      {
        LocalStorage = localStorage;
        RemoteStorage = remoteStorage;
      }
    }

    public class RemoteDataStorage : IDataStorage
    {
    }

    public interface IDataStorage
    {
    }

    public enum Storages
    {
      Local, Remote
    }
  }
}