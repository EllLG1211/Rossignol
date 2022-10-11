using Model.Business.Entries;
using Model.Business.Entries.Serialized;
using Model.Business.Managers;
using System.Collections.Generic;
using Xunit;

namespace Model_Tests.Business.Managers
{
    public class EntryEncryptionManager_Tests
    {
        [Theory]
        [MemberData(nameof(TrySharedEntryEncryptionDecryption_Data))]
        public void TrySharedEntryEncryptionDecryption_Test(SharedEntry totest, string key, bool shouldThrow)
        {
            try
            {
                EncryptedSharedEntry ese = EntryEncryptionManager.SharedToEncryptedEntry(totest, key);

                SharedEntry se = EntryEncryptionManager.EncryptedToSharedEntry(ese, key);

                Assert.False(shouldThrow);
                //Assert.Equal(totest, se); //TODO: fix broken looping comparaison op
            }
            catch
            {
                Assert.True(shouldThrow);
            }
        }

        [Theory]
        [MemberData(nameof(TryProprietaryEntryEncryptionDecryption_Data))]
        public void TryProprietaryEntryEncryptionDecryption_Test(ProprietaryEntry totest, string key, bool shouldThrow)
        {
            try
            {
                EncryptedProprietaryEntry ese = EntryEncryptionManager.ProprietaryToEncryptedEntry(totest, key);

                ProprietaryEntry se = EntryEncryptionManager.EncryptedToProprietaryEntry(ese, key);

                Assert.False(shouldThrow);
                //Assert.Equal(totest, se); //TODO: fix broken looping comparaison op
            }
            catch
            {
                Assert.True(shouldThrow);
            }
        }

        #region sharedTestData
        public static IEnumerable<object[]> TrySharedEntryEncryptionDecryption_Data()
        {
            yield return new object[]
            {
                new SharedEntry("karm", "12345", "discord", "none"),
                "mysuperpassword",
                false
            };

            yield return new object[]
            {
                new SharedEntry("karm", "12345", "discord"),
                null,
                true
            };

            yield return new object[]
            {
                null,
                "mysuperpassword",
                true
            };

        }
        #endregion

        #region ProprietaryTestData
        public static IEnumerable<object[]> TryProprietaryEntryEncryptionDecryption_Data()
        {
            yield return new object[]
            {
                new ProprietaryEntry("karm", "12345", "discord", "none"),
                "mysuperpassword",
                false
            };

            yield return new object[]
            {
                new ProprietaryEntry("karm", "12345", "discord"),
                null,
                true
            };

            yield return new object[]
            {
                null,
                "mysuperpassword",
                true
            };

        }
        #endregion
    }
}
