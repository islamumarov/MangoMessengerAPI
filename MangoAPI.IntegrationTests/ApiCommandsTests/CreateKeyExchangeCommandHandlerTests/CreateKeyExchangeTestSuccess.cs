﻿using System.Threading;
using System.Threading.Tasks;
using MangoAPI.BusinessLogic.ApiCommands.DiffieHellmanKeyExchanges;
using MangoAPI.IntegrationTests.Helpers;
using Xunit;

namespace MangoAPI.IntegrationTests.ApiCommandsTests.CreateKeyExchangeCommandHandlerTests;

public class CreateKeyExchangeTestSuccess : IntegrationTestBase
{
    private readonly Assert<CreateKeyExchangeResponse> assert = new();

    [Fact]
    public async Task OpenSslCreateKeyExchangeTest_Success()
    {
        var sender =
            await MangoModule.RequestAsync(CommandHelper.RegisterKhachaturCommand(), CancellationToken.None);
        var requestedUser =
            await MangoModule.RequestAsync(CommandHelper.RegisterPetroCommand(), CancellationToken.None);
        var command = CommandHelper.CreateOpenSslCreateKeyExchangeCommand(
            receiverId: sender.Response.UserId,
            senderId: requestedUser.Response.UserId,
            senderPublicKey: MangoFilesHelper.GetTestImage());

        var response = await MangoModule.RequestAsync(command, CancellationToken.None);

        assert.Pass(response);
    }
}
