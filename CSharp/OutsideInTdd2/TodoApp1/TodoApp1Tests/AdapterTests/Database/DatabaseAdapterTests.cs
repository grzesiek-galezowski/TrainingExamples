using ApplicationLogic.Ports;
using FluentAssertions;

namespace TodoApp1Tests.AdapterTests.Database;

public class DatabaseAdapterTests
{
    [Test]
    public async Task ShouldAllowAccessingSavedNoteByIdFromAnotherInstance()
    {
        //GIVEN
        var driver1 = DatabaseAdapterDriver.Default();
        var driver2 = DatabaseAdapterDriver.WithPath(driver1.FilePath);
        var noteDefinition = Any.Instance<NewTodoNoteDefinitionDto>();
        var noteMetadata = await driver1.Save(noteDefinition);

        //WHEN
        var noteById = await driver2.ReadNoteById(noteMetadata.Id);

        //THEN
        noteById.Title.Should().Be(noteDefinition.Title);
        noteById.Content.Should().Be(noteDefinition.Content);
        noteById.Id.Should().NotBeEmpty();
    }

    [Test]
    public async Task ShouldGenerateDifferentIdForSavedNoteEachTime()
    {
        //GIVEN
        var driver = DatabaseAdapterDriver.Default();
        var noteMetadata1 = await driver.Save(
          Any.Instance<NewTodoNoteDefinitionDto>());
        //WHEN
        var noteMetadata2 = await driver.Save(
          Any.Instance<NewTodoNoteDefinitionDto>());

        //THEN
        noteMetadata1.Id.Should().NotBeEmpty();
        noteMetadata2.Id.Should().NotBeEmpty();
        noteMetadata1.Id.Should().NotBe(noteMetadata2.Id);
    }

    [Test]
    public async Task ShouldAllowSavingAndReadingMultipleNotes()
    {
        //GIVEN
        var driver = DatabaseAdapterDriver.Default();
        var noteMetadata1 = await driver.Save(
          Any.Instance<NewTodoNoteDefinitionDto>());
        var noteMetadata2 = await driver.Save(
          Any.Instance<NewTodoNoteDefinitionDto>());

        //WHEN
        var note1ById = await driver.ReadNoteById(noteMetadata1.Id);
        var note2ById = await driver.ReadNoteById(noteMetadata2.Id);

        //THEN
        note1ById.Id.Should().Be(noteMetadata1.Id);
        note2ById.Id.Should().Be(noteMetadata2.Id);
    }
    //bug error scenarios (reading when file does not exist)
}