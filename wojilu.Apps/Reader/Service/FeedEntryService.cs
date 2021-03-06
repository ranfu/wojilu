/*
 * Copyright (c) 2010, www.wojilu.com. All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.Text;
using wojilu.Apps.Reader.Domain;
using wojilu.Apps.Reader.Interface;
using wojilu.Common.Jobs;

namespace wojilu.Apps.Reader.Service {

    public class FeedEntryService : IFeedEntryService {

        public virtual FeedEntry GetById(long id) {
            return db.findById<FeedEntry>( id );
        }

        public virtual FeedEntry GetByLink( String entryLink ) {
            return db.find<FeedEntry>( "Link=:link" ).set( "link", entryLink ).first();
        }


        public virtual DataPage<FeedEntry> GetPage(long feedId) {
            return db.findPage<FeedEntry>( "FeedSource.Id=" + feedId + " order by PubDate desc, Id desc" );
        }

        public virtual DataPage<FeedEntry> GetPage( String feedIds ) {
            if (strUtil.IsNullOrEmpty( feedIds )) return DataPage<FeedEntry>.GetEmpty();
            return db.findPage<FeedEntry>( "FeedSource.Id in(" + feedIds + ") order by PubDate desc, Id desc" );
        }

        public virtual void AddHits( FeedEntry item ) {
            HitsJob.Add( item );
        }

    }
}
