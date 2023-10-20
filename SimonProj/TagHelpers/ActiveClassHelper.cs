using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SimonProj.TagHelpers;

[HtmlTargetElement(Attributes = "active-class")]
public class ActiveClassTagHelper : TagHelper
{
    [HtmlAttributeName("active-class")]
    public string? ActiveClass { get; set; }

    [HtmlAttributeName("asp-controller")]
    public string? Controller { get; set; }

    [HtmlAttributeName("asp-action")]
    public string? Action { get; set; }

    private readonly IHttpContextAccessor _httpContextAccessor;

    public ActiveClassTagHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (Controller is null || Action is null || ActiveClass is null)
        {
            return;
        }

        if (IsCurrentControllerAction(Controller, Action))
        {
            output.Attributes.SetAttribute("class", ActiveClass);
        }
    }

    private bool IsCurrentControllerAction(string controller, string action)
    {
        var routeData = _httpContextAccessor.HttpContext.GetRouteData();
        var currentController = routeData.Values["controller"].ToString();
        var currentAction = routeData.Values["action"].ToString();

        return (controller == currentController && action == currentAction);
    }
}