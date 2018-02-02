using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace OLAP_WindowsForms.App
{
    class Operators
    {
        ///START Operators not involving comparison      
        //START Operators Changing Granularity Level 
        //TODO
        public void DrillDownOneLeve(Object d)
        {

        }

        //TODO
        public void DrillDownToLeve(Object d, Object g)
        {

        }

        //TODO
        public void RollUpOneLevel(Object d)
        {

        }

        //TODO
        public void RollUpToLevel(Object d, Object g)
        {

        }
        //END Operators Changing Granularity Level

        //START Operators Changing Dice Node
        //TODO
        public void MoveDownToFirstNode(Object d)
        {

        }

        //TODO
        public void MoveDownToLastNode(Object d)
        {

        }

        //TODO
        public void MoveToNextNode(Object d)
        {

        }

        //TODO !!!
        public void MoveToPrevNode(int dLvl)
        {

        }
        //END Operators Changing Dice Node

        //START Operators Changing Base Measure Conditions
        //TODO
        public void NarrowBMsrCond(List<Object> b)
        {

        }

        //TODO
        public void NarrowBMsrCOND(Object bOld, Object bNew)
        {

        }

        //TODO
        public void BroadenBMsrCond(List<Object> b)
        {

        }

        //TODO
        public void BroadenBMsrCond(Object bOld, Object bNew)
        {

        }

        //TODO
        public void RefocusBMsrCond(List<Object> b)
        {

        }

        //TODO
        public void RefocusBMsrCond(Object bOld, Object bNew)
        {

        }
        //END Operators Changing Base Measure Conditions
        ///END Operators not involving comparison


        ///START Operators involving comparison
        //START Operators Changing Measures
        //TODO
        public void AddMeasure(List<Object> m)
        {

        }

        public void DropMeasure(List<Object> m)
        {

        }

        public void RefocusMeasure(List<Object> m)
        {

        }

        public void RefocusMeasure(Object mOld, Object mNew)
        {

        }

        public void MoveDownToMeasure(Object mOld, Object mNew)
        {

        }

        public void MoveUpToMeasure(Object mOld, Object mNew)
        {

        }
        //END Operators Chanting Measures
        ///END Operators involving comparison


        ///START Use of analysis situations as cubes
        //START Operators Changing Cube Access
        //TODO !!!
        public void DrillAcrossToCube(Object c, Object b, Object m, Object f)
        {

        }
        //END Operators Changing Cube Access

        //START Operators Changing Comparison
        //TODO 
        public void Rerelate(Object[] op, Object j)
        {

        }

        //TODO 
        public void Rerelate(Object[] op, Object j, Object s, Object f)
        {

        }

        //TODO 
        public void Rerelate(Object[] op)
        {

        }

        //TODO
        public void Correlate(Object[] op, Object j)
        {

        }

        //TODO
        public void Correlate(Object[] op, Object j, Object s, Object f)
        {

        }

        //TODO
        public void Correlate(Object[] op)
        {

        }

        //TODO
        public void Rejoin(Object j)
        {

        }

        //TODO
        public void Rejoin(Object j, Object s, Object f)
        {

        }
        //END Operators Changing Comparison
        ///END Use of analysis situations as cubes


        ///START Operators unrelate and untarget
        //START Operators Dropping Comparison
        //TODO
        public void Unrelate()
        {

        }

        //TODO
        public void Untarget()
        {

        }
        //END Operators Dropping Comparison
        ///END Operators unrelate and untarget


        ///START Navigation operators
        //START Operators Changing Dice Node
        //TODO
        public void MoveDownToNode(Object d, Object l, Object n)
        {

        }

        //TODO
        public void MoveUpToNode(Object d, Object l)
        {

        }

        //TODO
        public void MoveAsideToNode(Object d, Object n)
        {

        }

        //TODO
        public void MoveToNode(Object d, Object l, Object n)
        {

        }
        //END Operators Changing Dice Node

        //START Operators Changing Slice Conditions
        //TODO
        public void NarrowSliceCond(Object d, List<Object> p)
        {

        }

        //TODO
        public void NarrowSliceCond(Object d, Object pOld, Object pNew)
        {

        }

        //TODO
        public void BroadenSliceCond(Object d, List<Object> p)
        {

        }

        //TODO
        public void BroadenSliceCond(Object d, Object pOld, Object pNew)
        {

        }

        //TODO
        public void RefocusSliceCond(Object d, List<Object> p)
        {

        }

        //TODO
        public void RefocusSliceCond(Object d, Object pOld, Object pNew)
        {

        }
        //END Operators Changing Slice Conditions

        //START Operators Changing Filters
        //TODO
        public void NarrowFilter(List<Object> f)
        {

        }

        //TODO
        public void NarrowFilter(Object fOld, Object fNew)
        {

        }

        //TODO
        public void BroadenFilter(List<Object> f)
        {

        }

        //TODO
        public void BroadenFilter(Object fOld, Object fNew)
        {

        }

        //TODO
        public void RefocusFilter(List<Object> f)
        {

        }

        //TODO
        public void RefocusFilter(Object fOld, Object fNew)
        {

        }
        //END Operators Changing Filters

        //START Operators Introducing Comparison
        //TODO !!!
        public void Relate(Object[] op, Object j, Object s, Object f)
        {

        }

        //TODO !!!
        public void Relate(Object j, Object s, Object f)
        {

        }

        //TODO
        public void Target(Object[] op, Object j, Object s, Object f)
        {

        }
        //END Operators Introducing Comparison

        //START Operators Changing Comparison
        //TODO
        public void Retarget(Object[] op, Object j)
        {

        }

        //TODO
        public void Retarget(Object[] op, Object j, Object s, Object f)
        {

        }

        //TODO
        public void Retarget(Object[] op)
        {

        }

        /*
        //TODO
        public void NarrowFilter(List<Object> f)
        {

        }

        //TODO
        public void NarrowFilter(Object fOld, Object fNew)
        {

        }

        //TODO
        public void BroadenFilter(List<Object> f)
        {

        }

        //TODO
        public void BroadenFilter(Object fOld, Object fNew)
        {

        }

        //TODO
        public void RefocusFilter(List<Object> f)
        {

        }

        //TODO
        public void RefocusFilter(Object fOld, Object fNew)
        {

        }
        */
        //END Operators Changing Comparison

        //START Use of Analysis Situations as Cubes
        //TODO
        public void UseAsCube(Object c, Object b, Object m)
        {

        }

        //TODO
        public void UseAsCube(Object c, Object b, Object d, Object l, Object a, Object m)
        {

        }
        //END Use of Analysis Situations as Cubes
        ///END Navigation operators

    }
}
