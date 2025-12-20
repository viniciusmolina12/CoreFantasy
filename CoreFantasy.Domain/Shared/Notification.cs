using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFantasy.Domain.Shared
{
    public class Notification
    {
        public Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public void AddError(string Context, string Message)
        {
            if (!this.errors.TryGetValue(Context, out var contextErrors))
            {
                contextErrors = new List<string>();
                this.errors[Context] = contextErrors;
            }

            contextErrors.Add(Message);
        }

        public Notification Append(Notification notification)
        {
            if (notification == null) return this;

            foreach (var (context, messages) in notification.errors)
                foreach (var message in messages)
                    AddError(context, message);

            return this;
        }


        public List<string> GetErrorsByContext(string context)
            => this.errors.TryGetValue(context, out var errors)
                ? errors
                : new List<string>();

        public bool HasErrors()
            => this.errors.Count > 0;

    }
}
