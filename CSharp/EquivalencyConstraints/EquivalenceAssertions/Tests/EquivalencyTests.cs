using DependencyUpdatePriorityScanner.EquivalenceAssertions.Tests.Fixture;
using EquivalencyConstraints.EquivalenceAssertions;

namespace DependencyUpdatePriorityScanner.EquivalenceAssertions.Tests;

public class SubsetEquivalencySpecification
{
    [Test]
    public void ShouldPassForObjectsCreatedTheSameWay()
    {
        Assert.That(1, Should.BeEquivalentTo(1));
        Assert.That(new object(), Should.BeEquivalentTo(new object()));
        Assert.That(
            new Person
            {
                Age = 1,
                Name = "Zenek"
            },
            Should.BeEquivalentTo(new Person
            {
                Age = 1,
                Name = "Zenek"
            }));
    }

    [Test]
    public void ShouldPassForDifferentObjects()
    {
        {
            var equivalenceException =
                Assert.Throws<AssertionException>(() => Assert.That(1, Should.BeEquivalentTo(2)));
            Assert.That(equivalenceException.Message,
                Is.EqualTo(
                    "  Assert.That(1, Should.BeEquivalentTo(2))\r\n  Expected: equivalent to 2\r\n  But was:  1\r\n"),
                equivalenceException.Message);
        }
        {
            var equivalenceException =
                Assert.Throws<AssertionException>(() => Assert.That(
                new Person
                {
                    Age = 2,
                    Name = "Zenek"
                },
                Should.BeEquivalentTo(new Person
                {
                    Age = 1,
                    Name = "Zenek"
                })));
            Assert.That(equivalenceException.Message, Is.EqualTo("  Assert.That(new Person\r\n                {\r\n                    Age = 2,\r\n                    Name = \"Zenek\"\r\n                }, Should.BeEquivalentTo(new Person\r\n                {\r\n                    Age = 1,\r\n                    Name = \"Zenek\"\r\n                }))\r\n  Expected: equivalent to DependencyUpdatePriorityScanner.EquivalenceAssertions.Tests.Fixture.Person\r\n  But was:  <DependencyUpdatePriorityScanner.EquivalenceAssertions.Tests.Fixture.Person>\r\n"), equivalenceException.Message);
        }
        Assert.That(
            new Person
            {
                Age = 1,
                Name = "Zenek"
            },
            Should.BeEquivalentTo(new Person
            {
                Age = 1,
                Name = "Zenek2"
            }));
    }

    [Test]
    public void ExtraProperties_ShouldNotBeEquivalent()
    {
        var expected = new Person { Name = "John", Age = 30 };
        var actual = new PersonWithExtra { Name = "John", Age = 30, Extra = "Extra" };
        Assert.That(actual, Should.BeEquivalentTo(expected));
    }

}

public class EquivalencyTests
{

    [Test]
    public void ObjectsWithCollections_ShouldBeEquivalent()
    {
        var expected = new ClassWithCollection { Numbers = [1, 2, 3] };
        var actual = new ClassWithCollection { Numbers = [1, 2, 3] };
        Assert.That(actual, Should.BeEquivalentTo(expected));
    }

    [Test]
    public void SameCollections_ShouldBeEquivalent()
    {
        var expected = new List<int> { 1, 2, 3 };
        var actual = new[] { 1, 2, 3 };
        Assert.That(actual, Should.BeEquivalentTo(expected));
    }

    [Test]
    public void DifferentCollections_ShouldNotBeEquivalent()
    {
        var expected = new List<int> { 1, 2, 3 };
        var actual = new List<int> { 1, 2, 4 };

        Assert.Throws<AssertionException>(() =>
        {
            Assert.That(actual, Should.BeEquivalentTo(expected));
        }, "Expected failure due to different elements");

    }

    [Test]
    public void NestedObjects_ShouldBeEquivalent()
    {
        var expected = new PersonWithAddress { Name = "John", Address = new Address { City = "New York" } };
        var actual = new PersonWithAddress { Name = "John", Address = new Address { City = "New York" } };
        Assert.That(actual, Should.BeEquivalentTo(expected));
    }

    [Test]
    public void DifferentTypesSameProperties_ShouldBeEquivalent()
    {
        var expected = new Person { Name = "John", Age = 30 };
        var actual = new Employee { Name = "John", Age = 30 };
        Assert.That(actual, Should.BeEquivalentTo(expected));
    }

    [Test]
    public void SameProperties_ShouldBeEquivalent()
    {
        var expected = new Person { Name = "John", Age = 30 };
        var actual = new Person { Name = "John", Age = 30 };
        Assert.That(actual, Should.BeEquivalentTo(expected));
    }

    [Test]
    public void DifferentProperties_ShouldNotBeEquivalent()
    {
        var expected = new Person { Name = "John", Age = 30 };
        var actual = new Person { Name = "Jane", Age = 30 };
        Assert.Throws<AssertionException>(() =>
        {
            Assert.That(actual, Should.BeEquivalentTo(expected), "Expected failure due to different Name");
        });
    }

    [Test]
    public void Ignore_Test()
    {
        var expected = new Person2
        {
            Name = "John",
            Children = [new Child { ID = 1, Name = "Alice" }]
        };

        var actual = new Person2
        {
            Name = "John",
            Children = [new Child { ID = 2, Name = "Alice" }]
        };

        Assert.That(actual, Should.BeEquivalentTo(expected, options => 
            options
                .ForCollection(a => a.Children)
                .Exclude(b => b.ID)));
    }
    
    [Test]
    public void Ignore_Order_Test()
    {
        var expected = new Person2
        { 
            Children = new List<Child> 
            { 
                new() { ID = 1, Name = "Alice" }, 
                new() { ID = 2, Name = "Bob" } 
            } 
        };
        var actual = new Person2 
        { 
            Children = new List<Child> 
            { 
                new() { ID = 3, Name = "Bob" }, 
                new() { ID = 4, Name = "Alice" } 
            } 
        };

        Assert.That(actual, Should.BeEquivalentTo(expected, options => 
            options.ForCollection(a => a.Children)
                .IgnoreOrder()
                .Exclude(c => c.ID)));
    }


}