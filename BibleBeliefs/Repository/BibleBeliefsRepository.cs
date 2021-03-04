using BibleBeliefs.Database;
using System.Linq;
using System.ComponentModel;

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
            var topics = context.Topics.Where(x => x.Id == topic.Id).FirstOrDefault();
            if (topics != null && topics.Beliefs.Count == 0)
            {
                context.Remove(topics);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        
        #endregion

        #region Beliefs

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

        #endregion

        #region Verses

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

        #endregion
    }
}
