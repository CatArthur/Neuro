import cv2
import matplotlib.pyplot as plt
from deepface import DeepFace

cap=cv2.VideoCapture(0)
if not cap.isOpened():
    raise IOError("cannot open webcam")
else:
    print("Webcam is watching you")

def force(em):
    try:
        f = open('emotion.txt', 'w')
        f.write(em)
        f.close()
    except PermissionError:
        pass


emotion = []
dom_emotion = ''
while True:
    try:
        ret,frame=cap.read()
        result = DeepFace.analyze(frame, actions=['emotion'])
        emotion.append(result['dominant_emotion'])
        if(len(emotion)==3):
            dom_emotion = max(set(emotion), key=emotion.count)
            del emotion[0]
        #dom_emotion = DeepFace.analyze(frame, actions=['emotion'])['dominant_emotion']
        if(dom_emotion=='neutral')|(dom_emotion=='happy'):
            force('0')
        if(dom_emotion=='sad'):
            force('1')
        if(dom_emotion=='angry'):
            force('2')
        if(dom_emotion=='surprise')|(dom_emotion=='fear'):
            force('3')
        #cv2.putText(frame, dom_emotion,(50,50),cv2.FONT_HERSHEY_SIMPLEX, 3,(0,0,255),2,cv2.LINE_4)
        cv2.imshow('Demo video', frame)
        
        if (cv2.waitKey(2)&0xFF==ord('q')):
            break
    except ValueError:
        pass
cap.release()
cv2.destroyAllWindows()

    
