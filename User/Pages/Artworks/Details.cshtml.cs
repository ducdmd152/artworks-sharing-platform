using ArtHubBO.DTO;
using ArtHubBO.Entities;
using ArtHubBO.Enum;
using ArtHubBO.Payload;
using ArtHubService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace User.Pages.Artworks
{
    public class DetailsModel : PageModel
    {
        private readonly IPostService postService;
        private readonly IInteractionService interactionService;
        
        public DetailsModel(IPostService postService, IInteractionService interactionService) : base()
        {
            this.postService = postService;
            this.interactionService = interactionService;
        }
        public Post Post { get; set; } = default!;
        public List<Post> ArtistSuggestion = default!;
        public List<Post> OtherSuggestion = default!;
        public bool IsReacted { get; set; }
        public bool IsBookmarked { get; set; }
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

        public async Task<IActionResult> OnPostReactAsync(int postId)
        {
            var userString = HttpContext.Session.GetString("CREDENTIAL");
            var user = userString != null ? JsonConvert.DeserializeObject<Account>(userString) : null;
            if (user == null)
            {
                return new UnauthorizedResult();
            }

            interactionService.ReactForPost(user.Email, postId);
            return new OkResult();
        }
    }
}
