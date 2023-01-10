using EncryptedModel.Business.Managers;
using EncryptedModel.Business.Users;
using Model.Business.Users;
using System.Collections.Generic;
using Xunit;


namespace Utils_Tests.Managers
{
    public class UserEncryptionManager_Tests
    {
        [Theory]
        [MemberData(nameof(TryLocalUserEncryptionDecryption_Data))]
        public void TrySharedEntryEncryptionDecryption_Test(bool espectedResult, LocalUser localUser, string password)
        {
            try
            {
                EncryptedLocalUser elu = UserEncryptionManager.LocalToEncryptedUser(localUser, password);
                LocalUser lu = UserEncryptionManager.EncryptedToLocalUser(elu, password);

                Assert.True(espectedResult);
                //Assert.Equal(localUser, lu);
            } catch
            {
                Assert.False(espectedResult);
            }
        }

        #region LocalUserTestData
        public static IEnumerable<object[]> TryLocalUserEncryptionDecryption_Data()
        {
            yield return new object[]
            {
                true,
                new LocalUser("password"),
                "masterPassword"
            };

            yield return new object[]
            {
                false,
                new LocalUser("password"),
                null
            };

            yield return new object[]
            {
                false,
                null,
                "masterPassword"
            };
        }
        #endregion

        [Theory]
        [MemberData(nameof(TryConnectedUserEncryptionDecryption_Data))]
        public void TryConnectedUserEncryptionDecryption_Test(bool espectedResult, ConnectedUser connectedUser, string password)
        {
            try
            {
                EncryptedConnectedUser ecu = UserEncryptionManager.ConnectedToEncryptedUser(connectedUser, password);
                ConnectedUser cu = UserEncryptionManager.EncryptedToConnectedUser(ecu, password);

                Assert.True(espectedResult);
                //Assert.Equal(connectedUser, cu);
            } catch
            {
                Assert.False(espectedResult);
            }
        }

        #region ConnectedUserTestData
        public static IEnumerable<object[]> TryConnectedUserEncryptionDecryption_Data()
        {
            yield return new object[]
            {
                true,
                new ConnectedUser("mail", "password"),
                "masterPassword"
            };

            yield return new object[]
            {
                false,
                new ConnectedUser("mail", "password"),
                null
            };

            yield return new object[]
            {
                false,
                null,
                "masterPassword"
            };
        }
        #endregion
    }
}
