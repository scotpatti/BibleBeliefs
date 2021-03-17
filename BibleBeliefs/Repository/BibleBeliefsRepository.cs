using BibleBeliefs.Database;
using System.Linq;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace BibleBeliefs.Repository
{
    public static class BibleBeliefsRepository
    {
        public static BibleBeliefsContext context = new BibleBeliefsContext();

        #region Topics
        /// <summary>
        /// Given a DTO it creates the topic in the database. Don't forget to 
        /// insert your DTO into the list OR reload the topics. NOTE: This will
        /// not keep any Id associated with this topic that is handed in. 
        /// Finally, it returns the Id value added. It is possible to have 
        /// multiple, identical topics with this create functionality.
        /// </summary>
        /// <param name="topic">Topic to add to the database</param>
        /// <returns></returns>
        public static long CreateTopic(TopicDTO topic)
        {
            Topics topics = new Topics();
            topics.Topic = topic.Topic;
            context.Add<Topics>(topics);
            context.SaveChanges();
            return topics.Id;
        }

        /// <summary>
        /// Returns a list of topics.
        /// </summary>
        /// <returns></returns>
        public static BindingList<TopicDTO> GetTopics()
        {
            var list = context.Topics.OrderBy(x => x.Topic).Select(x => new TopicDTO
            {
                Id = x.Id,
                Topic = x.Topic
            }).ToList();
            return new BindingList<TopicDTO>(list);
        }

        /// <summary>
        /// Updates the text of the topic identified by its Id.
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public static bool UpdateTopic(TopicDTO topic)
        {
            var topicToUpdate = context.Topics.Where(x => x.Id == topic.Id).FirstOrDefault();
            if (topicToUpdate != null)
            {
                topicToUpdate.Topic = topic.Topic;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if DTO was found and deleted. Returns false if the DTO
        /// was not found OR if the topic has beliefs associated with it. 
        /// </summary>
        /// <param name="topic">Topic to delete.</param>
        /// <returns>true -> success</returns>
        public static bool DeleteTopic(TopicDTO topic)
        {
            var topics = context.Topics.Include(a => a.Beliefs).Where(x => x.Id == topic.Id).FirstOrDefault();
            if (topics == null) return false;
            if (topics.Beliefs.Count() > 0) return false;
            context.Remove(topics);
            context.SaveChanges();
            return true;
        }

        #endregion

        #region Beliefs

        public static long CreateBelief(BeliefDTO belief)
        {
            Beliefs beliefs = new Beliefs();
            beliefs.Belief = belief.Belief;
            beliefs.TopicId = belief.TopicId;
            context.Add<Beliefs>(beliefs);
            context.SaveChanges();
            return beliefs.Id;
        }

        public static BindingList<BeliefDTO> GetBeliefs(long topicId)
        {
            var list = context.Beliefs.Where(f => f.TopicId == topicId).Select(x => new BeliefDTO
            {
                Id = x.Id,
                Belief = x.Belief,
                TopicId = x.TopicId
            }).ToList();
            return new BindingList<BeliefDTO>(list);
        }

        public static bool UpdateBelief(BeliefDTO belief)
        {
            var dbBelief = context.Beliefs.Where(x => x.Id == belief.Id).FirstOrDefault();
            if (dbBelief == null) return false;
            bool updated = false;
            if (dbBelief.TopicId != belief.TopicId)
            {
                dbBelief.TopicId = belief.TopicId;
                updated = true;
            }
            if (dbBelief.Belief != belief.Belief)
            {
                dbBelief.Belief = belief.Belief;
                updated = true;
            }
            if (updated)
            {
                context.SaveChanges();
            }
            return updated;
        }

        public static bool DeleteBelief(long beliefId)
        {
            var belief = context.Beliefs.Include(a => a.Verses).Where(x => x.Id == beliefId).FirstOrDefault();
            if (belief == null) return false;
            if (belief.Verses.Count > 0) return false;
            context.Beliefs.Remove(belief);
            context.SaveChanges();
            return true;
        }

        #endregion

        #region Verses
        public static long CreateVerse(VerseDTO verse)
        {
            Verses verses = new Verses();
            verses.BeliefId = verse.BeliefId;
            verses.Book = verse.Book;
            verses.Chapter = verse.Chapter;
            verses.VerseStart = verse.VerseStart;
            verses.VerseEnd = verse.VerseEnd;
            verses.Verse = verse.ToString();
            context.Add<Verses>(verses);
            context.SaveChanges();
            return verses.Id;
        }

        public static BindingList<VerseDTO> GetVerses(long beliefId)
        {
            var list = context.Verses.Where(f => f.BeliefId == beliefId).Select(x => new VerseDTO
            {
                Id = x.Id,
                VerseText = x.VerseText,
                Book = x.Book,
                Chapter = x.Chapter,
                VerseStart = x.VerseStart,
                VerseEnd = x.VerseEnd,
                BeliefId = x.BeliefId
            }).ToList();
            return new BindingList<VerseDTO>(list);
        }

        public static bool UpdateVerse(VerseDTO verse)
        {
            var dbVerse = context.Verses.Where(x => x.Id == verse.Id).FirstOrDefault();
            if (dbVerse == null) return false;
            bool updated = false;
            if (dbVerse.BeliefId != verse.BeliefId)
            {
                dbVerse.BeliefId = verse.BeliefId;
                updated = true;
            }
            if (dbVerse.Book != verse.Book)
            {
                dbVerse.Book = verse.Book;
                updated = true;
            }
            if (dbVerse.Chapter != verse.Chapter)
            {
                dbVerse.Chapter = verse.Chapter;
                updated = true;
            }
            if (dbVerse.VerseStart != verse.VerseStart)
            {
                dbVerse.VerseStart = verse.VerseStart;
                updated = true;
            }
            if (dbVerse.VerseEnd != verse.VerseEnd)
            {
                dbVerse.VerseEnd = verse.VerseEnd;
                updated = true;
            }
            if (updated)
            {
                context.SaveChanges();
            }
            return updated;
        }

        public static bool DeleteVerse(long verseId)
        {
            var verse = context.Verses.Where(x => x.Id == verseId).FirstOrDefault();
            if (verse == null) return false;
            context.Verses.Remove(verse);
            context.SaveChanges();
            return true;
        }
        #endregion
    }
}
