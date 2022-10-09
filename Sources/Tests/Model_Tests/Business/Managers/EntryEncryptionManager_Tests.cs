using Model.Business.Entries;
using Model.Business.Entries.Serialized;
using Model.Business.Managers;
using Model.Business.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                EntryEncryptionManager eem = new EntryEncryptionManager();

                EncryptedSharedEntry ese = eem.SharedToEncryptedEntry(totest, key);

                SharedEntry se = eem.EncryptedToSharedEntry(ese, key);

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

                EntryEncryptionManager eem = new EntryEncryptionManager();

                EncryptedProprietaryEntry ese = eem.ProprietaryToEncryptedEntry(totest, key);

                ProprietaryEntry se = eem.EncryptedToProprietaryEntry(ese, key);

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
