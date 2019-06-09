using ClassifiedAds.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ClassifiedAds.Persistence.MappingConfigurations
{
    public class SmsMessageConfiguration : IEntityTypeConfiguration<SmsMessage>
    {
        public void Configure(EntityTypeBuilder<SmsMessage> builder)
        {
            // Seed
            builder.HasData(new List<SmsMessage> {
                new SmsMessage
                {
                    Id = Guid.Parse("6672E891-0D94-4620-B38A-DBC5B02DA9F7"),
                    Message = "Hello",
                    PhoneNumber = "123456"
                },
                new SmsMessage
                {
                    Id = Guid.Parse("CC9D7ECA-6428-4E6D-B40B-2C8D93AB7347"),
                     Message = "World",
                    PhoneNumber = "123456"
                }
            });
        }
    }
}
