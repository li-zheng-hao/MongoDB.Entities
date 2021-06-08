﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace MongoDB.Entities.Tests
{
    [TestClass]
    public class Replace
    {
        [TestMethod]
        public async Task correct_doc_is_replaced()
        {
            var book = new Book { Title = "book title" };
            await book.SaveAsync();

            book.Title = "updated title";

            await DB.Replace<Book>()
                .MatchID(book.ID)
                .Match(b => b.Title == "book title")
                .WithEntity(book)
                .ExecuteAsync();

            var res = await DB.Find<Book>().OneAsync(book.ID);

            Assert.AreEqual(book.Title, res.Title);
        }

        [TestMethod]
        public async Task correct_docs_replaced_with_bulk_replace()
        {
            var book1 = new Book { Title = "book one" };
            var book2 = new Book { Title = "book two" };
            var books = new[] { book1, book2 };
            await books.SaveAsync();

            var cmd = DB.Replace<Book>();

            foreach (var book in books)
            {
                book.Title = book.ID;
                cmd.Match(b => b.ID == book.ID)
                   .WithEntity(book)
                   .AddToQueue();
            }

            await cmd.ExecuteAsync();

            var res1 = await DB.Find<Book>().OneAsync(book1.ID);
            var res2 = await DB.Find<Book>().OneAsync(book2.ID);

            Assert.AreEqual(book1.ID, res1.Title);
            Assert.AreEqual(book2.ID, res2.Title);
        }
    }
}