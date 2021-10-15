using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiteDB;
using OutsideInTdd.App;

namespace OutsideInTdd.Adapters
{
    public class TodoNoteDao : ITodoNoteDao, IDisposable
    {
        private readonly string _fileName;
        private readonly LiteDatabase _db;
        public static Random _r = new Random(Guid.NewGuid().GetHashCode());

        public TodoNoteDao()
        {
            _fileName = $"lol{_r.Next()}.db";
            _db = new LiteDatabase(_fileName);
        }

        public void Save(TodoNoteDto dto)
        {
            _db.GetCollection<TodoNoteDto>().Insert(dto);
        }

        public List<TodoNoteDto> LoadAllItems()
        {
            return _db.GetCollection<TodoNoteDto>().FindAll().ToList();

        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}