# Hand Tracking with Gesture Recognition 
## Multimodal Machine Learning
### National Centre of Scientific Research "Demokritos"
### Author: Alexandros Filios - mtn2219
###### [Installation guide inside the READ.ME of the project]
#### 1. Introduction
##### 1.1 Background and Motivation
Artificial intelligence (AI) has become an integral part of various technology sectors, including
entertainment, and more specifically, video games. The recent advancements in virtual reality (VR)
and augmented reality (AR) have created a demand for research into various sensory technologies
that can capture even the slightest nuances of human output and utilize it as input in digital
worlds. For example, during an interview on the Joe Rogan Experience, Mark Zuckerberg, the
current Chief Executive Officer of Meta as of May 2023, revealed that the Meta-verse team is
developing a mechanism that can detect the exact direction of a user's iris through VR glasses,
align it with the virtual character in the simulation, and create a more realistic result of interactions
with other users. Such innovative ideas highlight the significance of multimodal machine learning
for the evolution of gaming and the creation of immersive worlds. Thus, the opportunity to explore
such technologies and apply them to my project motivated my interest in the Mediapipe library.
##### 1.2 Overview of the Project
This project aims to showcase the potential of the Mediapipe library in conjunction with the
Unity3D engine. It is not a game in the traditional sense, but rather a simulation world that allows
users to interact solely through their laptop camera and hand gestures, which are detected using
Mediapipe's hand tracking feature. The project demonstrates how hand gesture recognition can
Alexandros Filios - mtn2219 1 of 6
be implemented using the data collected from Mediapipe, which enables the user to trigger
specific actions within the simulation environment. It primarily focuses on the recognition of
closed fist gestures for both hands. In addition to gesture recognition, the simulation world also
incorporates player rotation, which is controlled by the position of the user's hand. The project
highlights the potential of multimodal machine learning for creating engaging and interactive
simulations, and it demonstrates the versatility of the Mediapipe library for developing such
applications.
##### 1.3 Scope and Objectives
The main objective of this project is to demonstrate how the Mediapipe library can be effectively
utilized for multimodal machine learning applications, particularly in the context of hand gesture
recognition in a Unity3D simulation. In order to achieve this, a comprehensive understanding of
Mediapipe's Hand tracking feature was necessary to ensure optimal performance and resource
utilization. Furthermore, the project aimed to create a simulation environment that is fully
extensible, meaning that additional gesture combinations can be easily added through the
application of mathematical formulas to accurately calculate hand signs in each frame. The scope
of this project was limited to the recognition of closed fist gestures for both hands; however, the
framework developed can be extended to accommodate more complex gesture recognition tasks
in the future. Overall, the project aimed to showcase the potential of Mediapipe and Unity3D for
developing interactive and engaging simulations that incorporate multimodal machine learning
techniques.
##### 1.4 Structure of the Report
The report begins with an overview of the technologies used in this project. This section
discusses the theoretical foundations of Mediapipe's Hand tracking feature and Unity3D's game
engine, outlining their key features and capabilities, and highlighting the key considerations
involved in their integration.
The subsequent section provides a detailed account of the project's implementation. This section
discusses the design and development of the simulation world, including the creation of the
environment, the integration of Mediapipe's hand tracking feature, and the development of the
gesture recognition mechanism. It also details the technical considerations involved in achieving
optimal performance, including the use of parallel processing and the optimization of memory
usage.
The following section presents the results of the project, including an evaluation of the
performance of the gesture recognition mechanism, and an analysis of the simulation world's
overall effectiveness in achieving its objectives. This section also discusses the limitations of the
project and identifies opportunities for further research and development.
Finally, the report concludes with a discussion of future work and potential directions for further
research. It highlights the potential of multimodal machine learning and hand gesture recognition
for creating engaging and interactive simulations, and outlines some of the challenges and
opportunities that will need to be addressed in order to achieve this goal.
Alexandros Filios - mtn2219 2 of 6
#### 2. Methodology & Technologies
##### 2.1 Mediapipe
Mediapipe is an open-source, cross-platform, customizable framework for building multimodal
machine learning pipelines. The framework is designed to enable fast prototyping and deployment
of machine learning models on mobile, desktop, and server environments. The framework offers a
variety of tasks such as object detection, face detection, pose estimation, and hand landmark
detection, which are essential for various computer vision applications.
In this project, the MediaPipe Hand Landmarker task was used to detect the landmarks of the
hands in an image. This task enables the localization of key points of the hands and rendering of
visual effects over the hands. The task operates on image data with a machine learning (ML)
model as static data or a continuous stream and outputs hand landmarks in image coordinates,
hand landmarks in world coordinates, and handedness (left/right hand) of multiple detected
hands.
The hand landmark model bundle, which is a part of the Hand Landmarker task, detects the
keypoint localization of 21 hand-knuckle coordinates within the detected hand regions. The model
was trained on approximately 30K real-world images, as well as several rendered synthetic hand
models imposed over various backgrounds.
The hand landmarker model bundle contains a palm detection model and a hand landmarks
detection model. The Palm detection model locates hands within the input image, and the hand
landmarks detection model identifies specific hand landmarks on the cropped hand image
defined by the palm detection model.
Since running the palm detection model is time-consuming, the Hand Landmarker task uses the
bounding box defined by the hand landmarks model in one frame to localize the region of hands
for subsequent frames when in video or live stream running mode. The Hand Landmarker task
only re-triggers the palm detection model if the hand landmarks model no longer identifies the
presence of hands or fails to track the hands within the frame. This reduces the number of times
Hand Landmarker triggers the palm detection model, thereby improving the overall performance
of the system.
Alexandros Filios - mtn2219 3 of 6
##### 2.2 Unity Engine
Unity is a popular game engine that provides a rich set of tools and functionalities for game
development, virtual reality, and augmented reality applications. It is well-suited for this project as
it offers an intuitive interface for creating interactive 3D simulations and games. Additionally, Unity
has extensive documentation and a large community, making it easy to find solutions to problems
and integrate with other libraries and plugins.
Furthermore, Unity integrates seamlessly with the Mediapipe plugin for hand tracking, which
simplifies the process of implementing gesture recognition in a Unity project. Unity's capabilities,
combined with the Mediapipe plugin, make it an ideal platform for building interactive simulations
that leverage hand tracking and gesture recognition technology.
#### 3. Implementation & Results
##### 3.1 Setup
During the implementation of this project, we encountered several obstacles that had to be
addressed. After a successful installation on Windows 11 (with a failed attempt on macOS), we
explored the demo scenes that were provided. The whole body capturing, eye tracking, and hand
tracking were amazing utilities that sparked ideas for future implementations. However, we initially
faced a challenge where the hands were behaving like a mirror, which made no sense for an FPS
game. Therefore, we had to turn (mirror) the hands to achieve the desired effect.
Once we had the base of our game ready, we needed to create an environment and an interaction
with it. To create the environment, we used the Unity Terrain tool, which allowed us to sculpt a
valley to bound the player's space and view.
##### 3.2 Code
Unity uses C#, an object-oriented programming language, for its implementation. We used the
Mediapipe plugin for hand detection, but we also needed that information in our classes to notify
the user if one or both of the hands were missing from the camera view. Thus, a careful analysis of
the preexisting library code was necessary to extract that information and ensure that the correct
hand (right or left) is shown in the respective notification.
For gesture recognition, we loaded the 21 knuckles in a class called Hand for each hand and
made sure that those landmarks aligned correctly with the documentation labelling. Further
investigation revealed that since the hands were considered to be UI in the scene, the knuckles
had no rotation, so the only data we could use was their local position.
This method called CheckClosedFistGesture() is used for gesture recognition, specifically to
detect if the hand is in a closed fist gesture. It calculates the distance between the thumb and
pinky MCP joints, which is then used to normalize the fingertip distances. The distances between
Alexandros Filios - mtn2219 4 of 6
each fingertip and its corresponding MCP joint are calculated, then divided by the thumb-to-pinky
distance to get the normalized fingertip distances. These normalized distances are then compared
to a certain threshold value. If all of the normalized distances are less than the threshold, the
function returns true, indicating that a closed fist gesture has been detected. Otherwise, it returns
false. This method of gesture recognition is based on the distance between landmarks on the
hand and can be used to detect other gestures as well by adjusting the threshold values.
In addition to the gesture recognition functionality, a captivating magical effect has been
implemented in the scene to enhance user engagement. This effect is triggered when both hands
simultaneously make the closed fist gesture. As a result, all the rocks that are present on the
terrain floor lose their gravity temporarily and start floating while emitting a purple glow, giving the
user the experience of telekinesis akin to science fiction movies. Once the effect is over, the rocks
return to their initial state and fall on the ground.
Furthermore, to improve the user's viewing experience, a player rotation functionality has been
incorporated. This is achieved by checking the position of the wrist knuckle (the root of the
hands). If both hands are on the same side of the screen, the player and camera smoothly rotate,
providing the user with the freedom to explore the world from different angles. These features not
only add to the overall user experience but also demonstrate the versatility of the Unity Engine in
implementing complex functionality with ease.
Alexandros Filios - mtn2219 5 of 6
#### 4. Conclusion & Future work
In conclusion, the gesture recognition system developed in this project has demonstrated the
potential for creating engaging and immersive gaming experiences without the need for traditional
input devices. The use of machine learning algorithms, such as the hand tracking in combination
with techniques such as gesture recognition can enable players to use natural gestures and
movements to interact with virtual environments.
The object-oriented programming patterns used in this project also allow for easy expansion and
addition of new gestures and effects, making the system flexible and adaptable for future
development. Potential future work includes the addition of new powers and functionalities, such
as fire and lightning, as well as the integration of gesture-based movement controls for greater
freedom of exploration.
In general, the development of gesture-based gaming systems represents an exciting avenue for
exploration, particularly in the context of emerging technologies such as AR and VR. The potential
for immersive and intuitive gaming experiences using natural gestures and movements is vast,
and we look forward to seeing how this field evolves in the future.
Alexandros Filios - mtn2219 6 of 6
