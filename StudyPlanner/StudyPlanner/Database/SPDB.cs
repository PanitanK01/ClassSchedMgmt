using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using StudyPlanner.Models;

namespace StudyPlanner.Database
{
    public class SPDB
    {
        readonly SQLiteAsyncConnection _database;

        public SPDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
           // _database.DropTableAsync<Classroom>().Wait();
            _database.CreateTableAsync<Classroom>().Wait();
            _database.CreateTableAsync<Event>().Wait();
            _database.CreateTableAsync<Todo>().Wait();
            _database.CreateTableAsync<Note>().Wait();
            _database.CreateTableAsync<ImageData>().Wait();
            _database.CreateTableAsync<Course>().Wait();
           // _database.CreateTableAsync<Grade>().Wait();
          //  _database.CreateTableAsync<Profile>().Wait();

        }

        // ------------------------------ Save data to database ------------------------------


        public Task<int> Save(Classroom classroom)
        {
            return _database.InsertAsync(classroom);
        }
        public Task<int> Save(Course course)
        {
            return _database.InsertAsync(course);
        }

        public Task<int> Save(Event _event)
        {
            return _database.InsertAsync(_event);
        }
        public Task<int> Save(Todo todo)
        {
            return _database.InsertAsync(todo);
        }
        public Task<int> Save(Note note)
        {
            return _database.InsertAsync(note);
        }
        public Task<int> Save(ImageData image)
        {
            return _database.InsertAsync(image);
        }

        // ------------------------------ Get data from database ------------------------------

        public Task<List<Classroom>> GetClassrooms()
        {
            return _database.Table<Classroom>().OrderBy(c => c.Day).ThenBy(c => c.StartTime).ToListAsync();
        }

        public Task<List<Classroom>> GetClassrooms(DayOfWeek day)
        {
            return _database.Table<Classroom>().Where(c => c.Day == day).OrderBy(c => c.StartTime).ToListAsync();
        }
        public Task<Classroom> GetClassroom(int id)
        {
            return _database.Table<Classroom>().Where(c => c.ID == id).FirstOrDefaultAsync();
        }
      
        public Task<List<Event>> GetEvents()
        {
            return _database.Table<Event>().OrderBy(e => e.StartDate).ThenBy(e => e.StartTime).ToListAsync();
        }
        public Task<List<Event>> GetEvents(DateTime date)
        {
            return _database.Table<Event>().Where(e => e.StartDate <= date && date <= e.EndDate).ToListAsync();
        }
        public Task<Event> GetEvent(int id)
        {
            return _database.Table<Event>().Where(e => e.ID == id).FirstOrDefaultAsync();
        }

        public Task<List<Todo>> GetTodos(bool isComplete)
        {
            return _database.Table<Todo>().Where(t => t.IsComplete == isComplete).ThenByDescending(t => t.CreateDate).ToListAsync();
        }
        public Task<List<Todo>> GetTodos(bool isComplete, bool isImportant)
        {
            return _database.Table<Todo>().Where(t => t.IsComplete == isComplete && t.IsImportant == isImportant).ThenByDescending(t => t.CreateDate).ToListAsync();
        }
        public Task<Todo> GetTodo(int id)
        {
            return _database.Table<Todo>().Where(t => t.ID == id).FirstOrDefaultAsync();
        }
        public Task<List<Todo>> GetTodo()
        {
            return _database.Table<Todo>().OrderBy(e => e.StartDate).ThenBy(e => e.StartTime).ToListAsync();
        }
        public Task<List<Todo>> GetTodo(DateTime date)
        {
            return _database.Table<Todo>().Where(e => e.StartDate <= date && date <= e.EndDate).ToListAsync();
        }
        public Task<List<Note>> GetNotes()
        {
            return _database.Table<Note>().ToListAsync();
        }
        public Task<Note> GetNote(int id)
        {
            return _database.Table<Note>().Where(n => n.ID == id).FirstOrDefaultAsync();
        }

        public Task<ImageData> GetImage(Guid guid)
        {
            return _database.Table<ImageData>().Where(i => i.Guid == guid).FirstOrDefaultAsync();
        }

        public Task<int> ImageCount()
        {
            return _database.Table<ImageData>().CountAsync();
        }

        public Task<List<Course>> GetCourses()
        {
            return _database.Table<Course>().ToListAsync();
        }

        public Task<Course> GetCourse(int id)
        {
            return _database.Table<Course>().Where(c => c.ID == id).FirstOrDefaultAsync();
        }

        // ------------------------------ Update data to database ------------------------------

        public Task<int> UpdateClassroom(Classroom classroom)
        {
            return _database.UpdateAsync(classroom);
        }
        public Task<int> UpdateCourse(Course course)
        {
            return _database.UpdateAsync(course);
        }
        public Task<int> UpdateEvent(Event _event)
        {
            return _database.UpdateAsync(_event);
        }
        public Task<int> UpdateTodo(Todo todo)
        {
            return _database.UpdateAsync(todo);
        }
        public Task<int> UpdateNote(Note note)
        {
            return _database.UpdateAsync(note);
        }

        // ------------------------------ Delete data from database ------------------------------

        public Task<int> DeleteClassroom(Classroom classroom)
        {
            return _database.DeleteAsync<Classroom>(classroom.ID);
        }
        public Task<int> DeleteCourse(Course course)
        {
            return _database.DeleteAsync<Course>(course.ID);
        }
        public Task<int> DeleteEvent(Event _event)
        {
            return _database.DeleteAsync<Event>(_event.ID);
        }
        public Task<int> DeleteTodo(Todo todo)
        {
            return _database.DeleteAsync<Todo>(todo.ID);
        }
        public Task<int> DeleteNote(Note note)
        {
            return _database.DeleteAsync<Note>(note.ID);
        }
        public Task<int> DeleteImage(ImageData image)
        {
            return _database.DeleteAsync<ImageData>(image.Guid);
        }
        public Task<int> DeleteImage(Guid guid)
        {
            return _database.DeleteAsync<ImageData>(guid);
        }


        public Task<int> ClearTODO()
        {
            return _database.DeleteAllAsync<Todo>();
        }
    }
}
