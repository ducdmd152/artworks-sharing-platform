using ArtHubBO.Entities;
using ArtHubDAO.Interface;
using ArtHubRepository.Enum;
using ArtHubRepository.Interface;
using ArtHubService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHubService.Service
{    
    public class InteractionService : IInteractionService
    {
        private readonly IPostRepository postRepository;
        private readonly IReactionRepository reactionRepository;
        private readonly IBookmarkRepository bookmarkRepository;
        private readonly IDapperQueryService dapperQueryService;
        private readonly IUnitOfWork unitOfWork;

        public InteractionService(IPostRepository postRepository,
            IReactionRepository reactionRepository,
            IBookmarkRepository bookmarkRepository,
            IDapperQueryService dapperQueryService, 
            IUnitOfWork unitOfWork)
        {
            this.postRepository = postRepository;
            this.reactionRepository = reactionRepository;
            this.bookmarkRepository = bookmarkRepository;
            this.dapperQueryService = dapperQueryService;
            this.unitOfWork = unitOfWork;            
        }

        public bool CheckIsReactedForPost(string email, int postId)
        {
            return this.dapperQueryService.SingleOrDefault<bool>(
                QueryName.SelectIsReactedForPost,
                new
                {
                    AccountEmail = email,
                    PostId = postId
                }
            );
        }

        public bool CheckIsBookmarkedForPost(string email, int postId)
        {
            return this.dapperQueryService.SingleOrDefault<bool>(
                QueryName.SelectIsBookmarkedForPost,
                new
                {
                    AccountEmail = email,
                    PostId = postId
                }
            );
        }

        public async Task<bool> ReactForPost(string email, int postId)
        {
            if (this.CheckIsReactedForPost(email, postId))
                return true;
            Reaction reaction = new Reaction()
            {
                AccountEmail = email,
                PostId = postId
            };

            try
            {                
                await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await reactionRepository.AddAsync(reaction).ConfigureAwait(false);
                await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
            }
            return false;
        }

        public async Task<bool> BookmarkForPost(string email, int postId)
        {
            if (this.CheckIsBookmarkedForPost(email, postId))
                return true;
            Bookmark bookmark = new Bookmark()
            {
                AccountEmail = email,
                PostId = postId
            };

            try
            {
                await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await bookmarkRepository.AddAsync(bookmark).ConfigureAwait(false);
                await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
            }
            return false;
        }


        public async Task<bool> UnReactForPost(string email, int postId)
        {
            if (!this.CheckIsReactedForPost(email, postId))
                return true;
            Reaction reaction = this.reactionRepository.GetByCompositeKey(email, postId);

            try
            {
                await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                reactionRepository.Remove(reaction);
                await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
            }
            return false;
        }

        public async Task<bool> UnBookmarkForPost(string email, int postId)
        {
            if (!this.CheckIsBookmarkedForPost(email, postId))
                return true;
            Bookmark bookmark = bookmarkRepository.GetByCompositeKey(email, postId);

            try
            {
                await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                bookmarkRepository.Remove(bookmark);
                await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
            }
            return false;
        }


        public async Task<bool> OneMoreView(int postId)
        {
            var entity = postRepository.Get(postId);
            entity.TotalView = entity.TotalView + 1;
            try
            {
                await unitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                this.postRepository.Update(entity);
                await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTransaction();
            }
            return false;
        }
    }
}
