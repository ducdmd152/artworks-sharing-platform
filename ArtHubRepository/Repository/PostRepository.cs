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

        public async Task<List<Post>> GetAllPostBySearchConditionAsync(SearchPayload<PostSearchConditionDto> searchPayload)
        {

            var searchCondition = searchPayload.SearchCondition;
            var query = this.DbSet.Include(p => p.PostCategories).Include(item => item.Images).Include(item => item.Artist).Include(item => item.Artist.Account).AsQueryable();
            if (searchCondition != null)
            {
                if (searchCondition.ArtistEmail != null)
                {
                    query = query.Where(p => p.ArtistEmail.Equals(searchCondition.ArtistEmail));
                }

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
                    query = query.Where(p => p.Status == (int)searchCondition.PostStatus);
                }

                if (searchCondition.PostScope != null)
                {
                    query = query.Where(p => p.Scope == (int)searchCondition.PostScope);
                }

                if (searchCondition.CategoryId != null && searchCondition.CategoryId.Length > 0)
                {
                    query = query.Where(p => p.PostCategories.Any(pc => searchCondition.CategoryId.Contains(pc.CategoryId)));
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

                if (searchCondition.SortType != null)
                {
                    if (searchCondition.SortType.Equals(SortType.RECENT))
                    {
                        if (searchCondition.SortDirection != null)
                        {
                            if (searchCondition.SortDirection.Equals(SortDirection.ASC))
                            {
                                query = query.OrderBy(p => p.CreatedDate);
                            }
                            else
                            {
                                query = query.OrderByDescending(p => p.CreatedDate);
                            }
                        }
                    }
                    else if (searchCondition.SortType.Equals(SortType.FAVOURITE))
                    {
                        if (searchCondition.SortDirection != null)
                        {
                            if (searchCondition.SortDirection.Equals(SortDirection.ASC))
                            {
                                query = query.OrderBy(p => p.TotalReact);
                            }
                            else
                            {
                                query = query.OrderByDescending(p => p.TotalReact);
                            }
                        }
                    }
                }
            }

            searchPayload.PageInfo.TotalItems = query.Count();

            if (searchPayload.PageInfo != null)
            {                                
                searchPayload.PageInfo.TotalPages = (searchPayload.PageInfo.TotalItems + searchPayload.PageInfo.PageSize - 1) / searchPayload.PageInfo.PageSize;
                query = query
                    .Skip((searchPayload.PageInfo.PageNum - 1) * searchPayload.PageInfo.PageSize)
                    .Take(searchPayload.PageInfo.PageSize);
            }

            var result = await query.ToListAsync();
            return result;
        }

        public List<Post> GetAllPost()
        {
            return this.DbSet.Include(x => x.PostCategories).ToList();
        }

        public Post Get(int id) => this.DbSet.Include(item => item.Images).Include(item => item.PostCategories).Include(item => item.Artist.Account).FirstOrDefault(item => item.PostId == id);
    }
}
