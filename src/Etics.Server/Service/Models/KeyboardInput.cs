using WindowsInput.Native;

namespace Etics.Server.Service.Models;

public struct KeyboardInput
{
    public bool IsModifier { get; set; }
    public VirtualKeyCode VirtualKeyCode { get; set; }
}