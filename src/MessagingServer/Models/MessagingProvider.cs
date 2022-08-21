using System;
namespace MessagingServer.Models
{
	public abstract class MessagingProvider
	{
		public string? Tag { get; set; }
		public MessagingChannel? Channel { get; set; }
	}
}

