using AiMultiAgentUI.Interfaces;
using AiMultiAgentUI.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly IWorkflowEngine _engine;

    public string FormattedOutput { get; set; } = "";
    public string Requirements { get; set; } = "";
    public string Architecture { get; set; } = "";
    public string Code { get; set; } = "";
    public string Review { get; set; } = "";

    public IndexModel(IWorkflowEngine engine)
    {
        _engine = engine;
    }

    public async Task OnPost(string idea)
    {
        var result = await _engine.RunAsync(idea);

        Requirements = result.Requirements;
        Architecture = result.Architecture;
        Code = result.Code;
        Review = result.Review;

        FormattedOutput =
$"""
================ REQUIREMENTS ================
{result.Requirements}

================ ARCHITECTURE ================
{result.Architecture}

================ CODE ========================
{result.Code}

================ REVIEW ======================
{result.Review}
""";
    }
}