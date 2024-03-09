using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubDAO.Interface;
using ArtHubRepository.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtHubRepository.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(IBaseDAO<Post> baseDAO) : base(baseDAO)
        { 
        }

        public List<Post> GetAllPostBySearchCondition(SearchPayload<PostSearchConditionDto> searchPayload)
        {

            var searchCondition = searchPayload.SearchCondition;

            if (searchCondition != null)
            {
                var query = this.DbSet.AsQueryable().Where(p => p.ArtistEmail.Equals(searchCondition.ArtistEmail));

                if (searchCondition.CreatedDate != null)
                {
                    query = query.Where(p => p.CreatedDate >= searchCondition.CreatedDate);
                }

                if (!string.IsNullOrEmpty(searchCondition.Title))
                {
                    query = query.Where(p => p.Title.Contains(searchCondition.Title));
                }

                if (searchCondition.PostStatus != null)
                {
                    query = query.Where(p => p.Status.Equals(searchCondition.PostStatus.ToString()));
                }

                if (searchCondition.PostScope != null)
                {
                    query = query.Where(p => p.Scope.Equals(searchCondition.PostScope.ToString()));
                }

                if (searchCondition.ReactFrom != null)
                {
                    if (searchCondition.ReactTo != null)
                    {
                        query = query.Where(p => p.TotalReact >= searchCondition.ReactFrom && p.TotalReact <= searchCondition.ReactTo);
                    }
                    else
                    {
                        query = query.Where(p => p.TotalReact >= searchCondition.ReactFrom);
                    }
                }

                if (searchCondition.BookmarkFrom != null)
                {
                    if (searchCondition.BookmarkTo != null)
                    {
                        query = query.Where(p => p.TotalBookmark >= searchCondition.BookmarkFrom && p.TotalBookmark <= searchCondition.BookmarkTo);
                    }
                    else
                    {
                        query = query.Where(p => p.TotalBookmark >= searchCondition.BookmarkFrom);
                    }
                }

                if (searchCondition.ViewFrom != null)
                {
                    if (searchCondition.ViewTo != null)
                    {
                        query = query.Where(p => p.TotalView >= searchCondition.ViewFrom && p.TotalView <= searchCondition.ViewTo);
                    }
                    else
                    {
                        query = query.Where(p => p.TotalView >= searchCondition.ViewFrom);
                    }

                }
                
                if (searchCondition.SortDirection != null)
                {
                    if (searchCondition.SortDirection.Equals(SortDirection.ASC))
                    {
                        query = query.OrderBy(p => p.CreatedDate);
                    } else
                    {
                        query = query.OrderByDescending(p => p.CreatedDate);
                    }
                }

                query = query
                    .Skip((searchPayload.PageInfo.PageNum - 1) * searchPayload.PageInfo.PageSize)
                    .Take(searchPayload.PageInfo.PageSize);

                return query.ToList();
            }
            else
            {                
                return new List<Post>();
            }
        }
    }
}
