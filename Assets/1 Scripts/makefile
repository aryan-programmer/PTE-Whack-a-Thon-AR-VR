BEGIN_VALUES = "qc" --file
ENDING_FLAGS = --language CS

CheerleaderCoordinator.cs: CheerleaderCoordinator.qc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

GameManager.cs: GameManager.qc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

Hammer.cs: Hammer.qc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

Mole.cs: Mole.qc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

Player.cs: Player.qc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

VRUIButton.cs: VRUIButton.qc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

VRUIEventSystem.cs: VRUIEventSystem.qc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

all: CheerleaderCoordinator.cs, GameManager.cs, Hammer.cs, Mole.cs, Player.cs, VRUIButton.cs, VRUIEventSystem.cs
	echo "All done."

clean: 
	del CheerleaderCoordinator.cs GameManager.cs Hammer.cs Mole.cs Player.cs VRUIButton.cs VRUIEventSystem.cs