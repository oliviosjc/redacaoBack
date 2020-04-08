using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Redacao.Core.Services
{
	public class CoreServices : ICoreServices
	{
		public Guid? GetLoggedUserId(IHttpContextAccessor httpContextAccessor)
		{
			return Guid.NewGuid();
			//return new Guid(httpContextAccessor.HttpContext.User.Claims.ToList().FirstOrDefault(f => f.Type == "userId").Value);
		}
	}
}
