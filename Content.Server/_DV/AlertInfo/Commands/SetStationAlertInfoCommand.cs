using Content.Server.Administration;
using Content.Server.Station.Systems;
using Content.Shared.Administration;
using Robust.Shared.Console;

namespace Content.Server._DV.AlertInfo.Commands;

[AdminCommand(AdminFlags.Debug)]
public sealed class SetStationAlertInfoCommand : IConsoleCommand
{
    public string Command => "setstationtext";
    public string Description => "Sets the alert info displayed on all PDAs for a station";
    public string Help => "Usage: setstationtext <text>";

    public void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (args.Length < 1)
        {
            shell.WriteError("Need to provide a text");
            return;
        }

        var player = shell.Player;
        if (player == null || player?.AttachedEntity == null)
        {
            shell.WriteError("You need to be attached to an entity to use this command");
            return;
        }

        var message = string.Join(" ", args);
        var entityManager = IoCManager.Resolve<IEntityManager>();
        var stationSystem = entityManager.System<StationSystem>();
        var alertInfoSystem = entityManager.System<AlertInfoSystem>();

        var station = stationSystem.GetOwningStation(player.AttachedEntity.Value);

        if (station == null)
        {
            shell.WriteError("You must be on a station to use this command");
            return;
        }
        alertInfoSystem.SetAlertInfo(station.Value, message);
        shell.WriteLine("Status set!");
    }
}
