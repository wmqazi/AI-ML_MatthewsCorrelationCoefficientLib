using System;
using System.Collections.Generic;
using System.Text;

namespace Qazi.MachineLearningModels.MatthewsCorrelationCoefficientLib
{
    [Serializable]
    public class MatthewsCorrelationCoefficientCalculator
    {
        private float _ThresholdLevel;
        private float _PostiveLimit;
        private float _NegativeLimit;

        public float TP;
        public float TN;
        public float FP;
        public float FN;
        
        //Working Variables
        private float term1 ,term2;
        private string predictionResult;


        public MatthewsCorrelationCoefficientCalculator(float thresholdLevel ,float positiveLimit ,float negativeLimit)
        {
            _ThresholdLevel = thresholdLevel;
            _PostiveLimit = positiveLimit;
            _NegativeLimit = negativeLimit;
            Reset();
        }

        public void Reset()
        {
            TP = 0;
            FP = 0; 
            TN = 0;
            FN = 0;
            
        }

        public string Update(float t ,float y)
        {
            if (y > _ThresholdLevel && t == _PostiveLimit)
            {
                predictionResult = "True Positive Prediction";
                TP++;
            }
            else
            if (y > _ThresholdLevel && t == _NegativeLimit)
            {
                predictionResult = "False Positive";
                FP++;
            }
            else
            if (y <= _ThresholdLevel && t == _PostiveLimit)
            {
                predictionResult = "False Negative";
                FN++;
            }
            else
            if (y < _ThresholdLevel && t == _NegativeLimit)
            {
                predictionResult = "True Negative Prediction";
                TN++;
            }

            return predictionResult;
        }

        public float MCC {
            get
            {
                term1 = ((TP * TN) - (FP * FN));
                term2 = ((TP + FP) * (TP + FN) * (TN + FP) * (TN + FN));
                term2 = (float)Math.Sqrt(term2);
                return (term1 / term2);
            }
        }

        public float Sensitivity
        {
            get
            {
                return (TP / (TP + FN));
            }
        }

        public float Specificity
        {
            get
            {
                return (TN / (FP + TN));
            }
        }

        public float Accuracy
        {
            get
            {
                return ((TP + TN)/(TP + FP + TN + FN));
            }
        }

    }
}
