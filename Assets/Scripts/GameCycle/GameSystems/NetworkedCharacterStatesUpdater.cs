using MeatInc.ActionGunnersClient.Interfaces.GameCycle;
using MeatInc.ActionGunnersShared.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersClient.GameCycle.GameSystems
{
    public class NetworkedCharacterStatesUpdater : BaseEntitySystemHandler<CharacterStateData>
    {
        public NetworkedCharacterStatesUpdater(IEntityComponentContainer<CharacterStateData> container) : base(container)
        {
            
        }

        public override void HandleStates(CharacterStateData[] states)
        {
            foreach (var state in states)
            {
                foreach (var component in Container.Components)
                {
                    if (state.Id == component.Id)
                    {
                        component.Handle(state);
                    }
                }
            }
        }
    }
}
