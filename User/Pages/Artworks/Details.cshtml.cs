using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace User.Pages.Artworks
{
    public class DetailsModel : PageModel
    {
        private readonly IPostService postService;
        
        public DetailsModel(IPostService postService) : base()
        {
            this.postService = postService;
        }

        public Post Post { get; set; } = default!;
        public List<Post> ArtistSuggestion = default!;
        public List<Post> OtherSuggestion = default!;
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!int.TryParse(id, out int postId))
            {
                return NotFound();
            }

            var item = postService.Get(postId);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                Post = item;

                ArtistSuggestion = await postService.GetAllPostBySearchConditionAsync(new SearchPayload<PostSearchConditionDto>()
                {
                    PageInfo = new PageInfo()
                    {
                        PageNum = 1,
                        PageSize = 3,
                    },
                    SearchCondition = new PostSearchConditionDto()
                    {
                        ArtistEmail = item.ArtistEmail,
                    }
                });

                OtherSuggestion = await postService.GetAllPostBySearchConditionAsync(new SearchPayload<PostSearchConditionDto>()
                {
                    PageInfo = new PageInfo()
                    {
                        PageNum = 1,
                        PageSize = 4,
                    },
                    SearchCondition = new PostSearchConditionDto()
                    {
                        PostScope = PostScope.Public,
                        PostStatus = PostStatus.Approval,
                        SortType = SortType.FAVOURITE,
                        SortDirection = SortDirection.DESC,
                        //CategoryId = Post.PostCategories.Select(item => item.CategoryId).ToArray(),
                    }
                });
            }
            return Page();
        }
    }
}
