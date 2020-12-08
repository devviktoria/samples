# Mouse Simulator Requirement Specification

This is the Requirement Specification for the Mouse Simulator Application.

## How cats are "playing" with mouses
When a cat catches a mouse for a while he holds it in his mouth or grabs it with his legs. After a short while he lets the mouse go. In that moment the mouse, of course, runs for his life. Then the cat is grabbing him again. This cycle happens while the mouse is alive.

## The goal of the app
This application is a cat toy app. The goal of the app is to simulate the above mouse behavior for cats.


## Users
the player - who is intended to be a cat 
the human user - who can control the app's behavior or change the app's settings

## Requirements

### 1. Application startup

1. The application has a splash screen which shows while the application is initializing.

1. During startup the configured image shall be loaded if the chosen image file is existing. If the chosen image file is not existing the default built in image shall be used for the play area's background. See also the [Background changer dialogue](#backgroundchangerdialogue) section.

### 2. The running application

1. A mouse shape moving around continually on the screen. 

1. If the player touches the mouse shape the mouse shape stops moving around. If the player no longer touches the mouse shape it shall continue to move around on the screen.

### 3. Application look and feel

1. The application has a top bar that contains the application's title.

1. The play area shall have a background image. See also the [Background changer dialogue](#backgroundchangerdialogue) section.

### 4. Application menu

The application has a menu with the following menu elements:

1. Stop - If the human user taps on this the mouse shape stops moving, and the label of this menu item changes to Start.

1. Start - If the human user taps on this the mouse shape starts moving, and the label of this menu item changes to Stop.

1. Slow - If the human user taps on this the animation of the mouse shape becomes slow [TODO: Define the exact timing of slow animation.]

1. Normal - If the human user taps on this the animation of the mouse shape becomes normal speed [TODO: Define the exact timing of normal speed animation.]

1. Fast - If the human user taps on this the animation of the mouse shape becomes fast [TODO: Define the exact timing of fast animation.]

1. Background - If the human user taps on this the mouse shape stops moving, and the background changing dialog appears.

### <a name="backgroundchangerdialogue"></a> 5. Background changer dialogue

1. The human user shall be able to choose between built in images and images store on the local device. The chosen image shall be used for the play area's background.