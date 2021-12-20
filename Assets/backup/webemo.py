import cv2
import matplotlib.pyplot as plt
from deepface import DeepFace

cap=cv2.VideoCapture(0)
if not cap.isOpened():
    raise IOError("cannot open webcam")
else:
    print("Webcam is watching you")

# f = open('stop.txt', 'w')
# f.write('')
# f.close()

ret,frame=cap.read()
emotion = []
dom_emotion = ''
bars = [0.25,0.25,0.25,0.25]
step=0.1


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

        print(str([round(x,2) for x in bars])[1:-1].replace(',',''))
        # try:
        #     f = open('emotion.txt', 'w')
        #     f.write(str([round(x,2) for x in bars])[1:-1].replace(',',''))
        #     f.close()
        # except PermissionError:
        #     pass
    except ValueError:
        pass
    # f = open('stop.txt')
    # if f.read() == 'q':
    #     break
    # f.close();
cap.release()
cv2.destroyAllWindows()

    
