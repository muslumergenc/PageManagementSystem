using PageManagementSystem.Core.Entities;

namespace PageManagementSystem.UI.Models.VMs.PageVMs;

public class PageViewModel
{
    public Page Page { get; set; }
    public IEnumerable<PageData> PageDataList { get; set; }
    public PageContent[] PageContent { get; set; }
}
