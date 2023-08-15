using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{

    public class Notes
    {
        public List<NoteModel> notes { get; set; }

        public Notes()
        {
            notes = new List<NoteModel>();
        }

        public void AddTask(string s)
        {
            NoteModel note = new NoteModel();
            note.Id = notes.Count + 1;
            note.Status = Status.InProgress;
            note.Name = s;
            notes.Add(note);
        }

        public List<NoteModel> GetNotes()
        {
            return notes;
        }

        public void ChangeStatus(int id, Status status)
        {
            try
            {
                if (id >= 0 && id < notes.Count)
                {
                    NoteModel noteToUpdate = notes[id];
                    noteToUpdate.Status = status;
                    notes[id] = noteToUpdate;
                }
                else
                {
                    Console.WriteLine("Некорректный id заметки или статус для смены");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void CloseTask(int id)
        {
            if (id >= 0 && id < notes.Count) {
                notes.RemoveAt(id);
            } else {
                Console.WriteLine("Некорректный id заметки");
            }
        }

        public List<NoteModel> SearchWithStatus(Status n)
        {
            List<NoteModel> list = new List<NoteModel>();
            foreach (NoteModel note in notes)
            {
                if (note.Status == n)
                {
                    list.Add(note);
                }
            }
            return list;
        }

        public NoteModel? SearchWithName(string s)
        {
            if (s == null) return null;
            foreach (NoteModel note in notes)
            {
                if(note.Name==s)
                    return note;
            }
            return null;
        }

        public NoteModel? SearchWithId(int n)
        {
            if (n >= 0 && n < notes.Count)
            {
                return notes[n];
            }
            else
            {
                Console.WriteLine("Некорректный id заметки");
            }
            return null;
        }

    }
}
