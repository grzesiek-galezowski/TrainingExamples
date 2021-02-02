using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OutsideInTdd;
using TddXt.AnyRoot.Strings;
using static TddXt.AnyRoot.Root;

namespace OutsideInTddBoxSpecification
{
    public class Tests
    {
        //TODO different users
        //TODO validate title and content (e.g. length)

        [Test]
        public async Task ShouldIncludeAddedNoteInAllNotes()
        {
            //GIVEN
            using var driver = new AppDriver();
            var title = Any.String();
            var content = Any.String();

            await driver.AddTodoNote(title, content);
            
            //WHEN
            var allNotes = await driver.RetrieveAllNotes();

            //THEN
            allNotes.ShouldConsistOfSingleNoteWith(title, content);
        }
    }
}