using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using HuaSect_AMS_DBTCclasslib.Interfaces;

public static class EncryptionConventionExtensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SkipEncryptionAttribute : Attribute { }

    public static void AddGlobalStringEncryption(this ModelBuilder modelBuilder, IEncryptionService encryption)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.IsOwned() || entityType.ClrType == null) continue;

            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType != typeof(string)) continue;
                
                if (property.GetValueConverter() != null) continue;
                
                if (property.IsPrimaryKey() || property.IsForeignKey()) continue;

                var clrProperty = property.PropertyInfo;
                if (clrProperty != null && clrProperty.GetCustomAttribute<SkipEncryptionAttribute>() != null)
                {
                    continue;
                }

                var converter = new ValueConverter<string?, string?>(
                    v => v == null ? null : encryption.Encrypt(v),
                    v => v == null ? null : encryption.Decrypt(v)
                );

                property.SetValueConverter(converter);
            }
        }
    }
}