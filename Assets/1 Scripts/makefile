BEGIN_VALUES = "qc" --file
ENDING_FLAGS = --language CS

ARVRObjectEnableControl.cs: ARVRObjectEnableControl.uqc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

CheerleaderCoordinator.cs: CheerleaderCoordinator.uqc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

GameManager.cs: GameManager.uqc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

Hammer.cs: Hammer.uqc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

Mole.cs: Mole.uqc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

SceneSwitcher.cs: SceneSwitcher.uqc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

Speaker.cs: Speaker.uqc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

WebCamDisplay.cs: WebCamDisplay.uqc
	$(BEGIN_VALUES) $< $(ENDING_FLAGS)

all: ARVRObjectEnableControl.cs CheerleaderCoordinator.cs GameManager.cs Hammer.cs Mole.cs SceneSwitcher.cs Speaker.cs WebCamDisplay.cs 
	echo "All done."

clean: 
	del ARVRObjectEnableControl.cs CheerleaderCoordinator.cs GameManager.cs Hammer.cs Mole.cs SceneSwitcher.cs Speaker.cs WebCamDisplay.cs 