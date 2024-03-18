using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Interface
{
    public interface IInteractionService
    {
        public bool CheckIsReactedForPost(string email, int postId);
        public bool CheckIsBookmarkedForPost(string email, int postId);
        public Task<bool> ReactForPost(string email, int postId);
        public Task<bool> BookmarkForPost(string email, int postId);
    }
}
