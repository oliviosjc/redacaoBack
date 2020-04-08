using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redacao.Core.Services
{
	public interface ICoreServices
	{
		Guid? GetLoggedUserId(IHttpContextAccessor httpContextAccessor);
	}
}
