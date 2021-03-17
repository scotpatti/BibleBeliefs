using System;
using System.ComponentModel;
using Microsoft.Data.Sqlite;

namespace BibleBeliefs.Repository
{

    /// <summary>
    /// This repository is a bit different than the other one since I don't use
    /// EF core. Instead I create direct connections to the database, using the 
    /// package Microsoft.Data.Sqlite, because this is a read only database. 
    /// </summary>
    internal class BibleRepository
    {
        private string _dbConnString;
        private SqliteConnection _Conn;
        public BibleRepository(string Version = "kjv")
        {
            _dbConnString = $"DataSource=.\\DataBase\\{Version}.db;";
        }

        private SqliteConnection GetConnection()
        {
            if (_Conn != null) return _Conn;
            _Conn = new SqliteConnection(_dbConnString);
            _Conn.Open();
            return _Conn;
        }

        internal BindingList<string> GetBooks()
        {
            BindingList<string> rval = new BindingList<string>();
            var conn = GetConnection();
            var comm = new SqliteCommand("SELECT DISTINCT c0book FROM bible_fts_content ORDER BY docid;", conn);
            var reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    rval.Add(reader.GetString(0));
                }
            }
            reader.Close();
            return rval;
        }

        internal BindingList<int> GetChapters(string book)
        {
            BindingList<int> rval = new BindingList<int>();
            var conn = GetConnection();
            var comm = new SqliteCommand($"SELECT Max(CAST(c1chapter as NUMERIC)) FROM bible_fts_content WHERE c0book = '{book}'; ", conn);
            int lastChapter = Convert.ToInt32(comm.ExecuteScalar());
            for (int i=1; i<=lastChapter; i++ )
            {
                rval.Add(i);
            }
            return rval;
        }

        internal BindingList<int> GetVerses(string book, int chapter)
        {
            BindingList<int> rval = new BindingList<int>();
            var conn = GetConnection();
            var comm = new SqliteCommand($"SELECT Max(CAST(c2verse as NUMERIC)) FROM bible_fts_content WHERE c0book = '{book}' AND c1chapter = '{chapter+1}'; ", conn);
            int lastVerse = Convert.ToInt32(comm.ExecuteScalar());
            for (int i=0; i<lastVerse; i++)
            {
                rval.Add(i+1);
            }
            return rval;
        }
    }
}
