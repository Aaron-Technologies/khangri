
using Microsoft.AspNetCore.Mvc;
using Khangri.UI.Helpers;

namespace Khangri.UI.Components
{
	public class ContactBarComponent : ViewComponent
	{
		private readonly IContactHelper _contactHelper;
		public ContactBarComponent(IContactHelper contactHelper)
		{
			_contactHelper = contactHelper;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var msg = String.Empty;
			var allContact=_contactHelper.GetAllContact(0,ref msg);
			return View(allContact);
		}
	}
}
