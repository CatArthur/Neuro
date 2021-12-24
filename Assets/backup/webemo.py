import cv2
import matplotlib.pyplot as plt
from deepface import DeepFace

cap=cv2.VideoCapture(1)
if not cap.isOpened():
    raise IOError("cannot open webcam")
else:
    print("Webcam is watching you")

emotion = []
dom_emotion = ''
bars = [1,0,0,0]
step=1


def force(em):
    for i in range(4):
        if(bars[i]>=step):
            bars[i]-=step
            bars[em]+=step
        else:
            bars[em]+=bars[i]
            bars[i]=0

while True:
    try:
        ret,frame=cap.read()
        gray=cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
        result = DeepFace.analyze(frame, actions=['emotion'])
        emotion.append(result['dominant_emotion'])
        if(len(emotion)==4):
            dom_emotion = max(set(emotion), key=emotion.count)
            del emotion[0]
        if(dom_emotion=='neutral')|(dom_emotion=='happy'):
            force(0)
        if(dom_emotion=='sad'):
            force(1)
        if(dom_emotion=='angry'):
            force(2)
        if(dom_emotion=='surprise')|(dom_emotion=='fear'):
            force(3)

        cv2.putText(frame, dom_emotion,(50,50),cv2.FONT_HERSHEY_SIMPLEX, 3,(0,0,255),2,cv2.LINE_4)
        
        cv2.rectangle(frame, (50, 400-int(bars[0]*100)), (70, 400), (0, 255, 255), -1)        
        cv2.rectangle(frame, (80, 400-int(bars[1]*100)), (100, 400), (255, 0, 0), -1)        
        cv2.rectangle(frame, (110,400-int(bars[2]*100)), (130, 400), (0, 0, 255), -1)        
        cv2.rectangle(frame, (140,400-int(bars[3]*100)), (160, 400), (255, 0, 255), -1)    
        cv2.rectangle(frame, (45, 400), (165, 405), (0, 0, 0), -1)
        
        cv2.imshow('Demo video', frame)
        try:
            f = open('emotion.txt', 'w')
            f.write(str([round(x,2) for x in bars])[1:-1].replace(',',''))
            f.close()
        except PermissionError:
            pass
        
    except ValueError:
        pass
    if (cv2.waitKey(2)&0xFF==ord('q')):
        break
cap.release()
cv2.destroyAllWindows()

    
