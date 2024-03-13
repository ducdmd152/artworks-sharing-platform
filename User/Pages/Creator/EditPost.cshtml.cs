using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace User.Pages.Creator;

public class EditPostModel : PageModel
{
    public void OnGet(int? postId)
    {
        Console.WriteLine(postId.ToString());
    }
}
