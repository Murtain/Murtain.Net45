using System;

using log4net.Appender;
using log4net.Core;

using Murtain.Caching;
using Murtain.RedisCache;
using Murtain.Dependency;
using Murtain.RedisCache.Configuration;
using Autofac;

namespace Murtain.Redis4net.Appender
{

    public class RedisAppender : AppenderSkeleton
    {
        private RedisCacheManager redisCacheManager;
        public RedisAppender()
        {
            
        }
        public string ListName { get; set; }
        public override void ActivateOptions()
        {
            base.ActivateOptions();
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            try
            {
                this.redisCacheManager = IocManager.Instance.Resolve<RedisCacheManager>();
                var message = RenderLoggingEvent(loggingEvent);
                redisCacheManager.Database.ListRightPushAsync(ListName, new StackExchange.Redis.RedisValue[] { message });
            }
            catch (Exception exception)
            {
                ErrorHandler.Error("Unable to send logging event to remote redis", exception, ErrorCode.WriteFailure);
            }
        }
    }
}
