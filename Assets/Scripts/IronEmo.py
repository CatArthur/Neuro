import cv2
import matplotlib.pyplot as plt
from deepface import DeepFace

class IronEmo():
    def __init__(self, name):
        self.cap=cv2.VideoCapture(0)
        if not self.cap.isOpened():
            raise IOError("cannot open webcam")
        else:
            print("Webcam is watching you")

        f = open('stop.txt', 'w')
        f.write('')
        f.close()

        self.ret,self.frame=cap.read()
        self.emotion = []
        self.dom_emotion = ''
        self.bars = [0.25,0.25,0.25,0.25]
        self.step=0.1


    def force(self,em):
        for i in range(4):
            if(self.bars[i]>=self.step):
                self.bars[i]-=self.step
                self.bars[em]+=self.step
            else:
                self.bars[em]+=self.bars[i]
                self.bars[i]=0

    def getBars(self):
        try:
            self.ret,self.frame=self.cap.read()
            result = DeepFace.analyze(frame, actions=['emotion'])
            self.emotion.append(result['dominant_emotion'])
            if(len(self.emotion)==4):
                self.dom_emotion = max(set(self.emotion), key=self.emotion.count)
                del self.emotion[0]
            if(self.dom_emotion=='neutral')|(self.dom_emotion=='happy'):
                self.force(0)
            if(self.dom_emotion=='sad'):
                self.force(1)
            if(self.dom_emotion=='angry'):
                self.force(2)
            if(self.dom_emotion=='surprise')|(self.dom_emotion=='fear'):
                self.force(3)

            try:
                f = open('emotion.txt', 'w')
                f.write(str([round(x,2) for x in bars])[1:-1].replace(',',''))
                f.close()
            except PermissionError:
                pass
        except ValueError:
            pass
        return self.bars

    def destroy(self):
        self.cap.release()
        cv2.destroyAllWindows()

        
