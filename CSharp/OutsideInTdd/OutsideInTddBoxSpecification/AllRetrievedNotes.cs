using System.Collections.Generic;
using FluentAssertions;
using OutsideInTdd.App;

namespace OutsideInTddBoxSpecification
{
    public class AllRetrievedNotes
    {
        private readonly IEnumerable<TodoNoteDto> _allNotes;

        public AllRetrievedNotes(IEnumerable<TodoNoteDto> allNotes)
        {
            _allNotes = allNotes;
        }

        public void ShouldConsistOfSingleNoteWith(string title, string content)
        {
            AssertionExtensions.Should(_allNotes).BeEquivalentTo(new List<TodoNoteDto>()
            {
                new TodoNoteDto(title, content)
            });
        }
    }
}