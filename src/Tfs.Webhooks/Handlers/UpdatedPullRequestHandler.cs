namespace Tfs.WebHooks.Handlers
{
    using Microsoft.AspNet.WebHooks.Payloads;
    using Serilog;
    using System.Threading.Tasks;

    public class UpdatedPullRequestHandler
    {

        public UpdatedPullRequestHandler()
        {
        }

        public async Task Handle(GitPullRequestUpdatedPayload pullRequest)
        {
        }
    }
}