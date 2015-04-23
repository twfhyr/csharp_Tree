  public interface ITreeNode {
       void AddChild(ITreeNode newChild);
        void Remove();
    }
     
    public class TreeNode: ITreeNode {
       public ITreeNode ParentNode = null;
   8:     public List<ITreeNode> Childs = new List<ITreeNode>();
   9:  
  10:     public void AddChild(ITreeNode newChild) {
  11:         ((TreeNode)newChild).ParentNode = this;
  12:         this.Childs.Add(newChild);
  13:     }
  14:  
  15:     public void Remove() {
  16:         foreach (ITreeNode childNode in this.Childs) {
  17:             childNode.Remove();
  18:         }
  19:         ((TreeNode)this.ParentNode).Childs.Remove(this);
  20:     }
  21: }
樹狀結構其實很簡單，針對一個節點來看就是一對多的父子關係，而子節點必須要知道父節點，這樣才能夠達到樹遍覽的目的。原本的想法只是用一個類就能解決，實做下去才發覺當遇到需要引用樹狀結構的節點時，這個類往往都還要繼承別的類，而C#並不允許同時繼承兩個類，但是可以有多個介面同時繼承，因此介面就在這裡發揮作用，新做的類是繼承介面，而不是繼承類。

   1: public class TreeFlowNode : FlowNodeBase, ITreeNode {
   2:     private TreeNode treeNode = new TreeNode();
   3:  
   4:     public TreeFlowNode() {
   5:     }
   6:  
   7:     public TreeFlowNode(FlowTargetType targetType, string empNo,
   8:         string teamNo, string postNo, FlowSignType signType) {
   9:         base.TargetType = targetType;
  10:         base.EmpNo = empNo;
  11:         base.TeamNo = teamNo;
  12:         base.PostNo = postNo;
  13:         base.SignType = signType;
  14:     }
  15:  
  16:     public void AddChild(ITreeNode flowNode) {
  17:         this.treeNode.AddChild((ITreeNode)flowNode);
  18:     }
  19:  Technorati 的標籤: C#,樹狀結構
  20:     public void Remove() {
  21:         this.treeNode.Remove();
  22:     }
  23: }
  24:  
  25: public class FlowNodeBase {
  26:     public FlowTargetType TargetType;
  27:     public string EmpNo;
  28:     public string TeamNo;
  29:     public string PostNo;
  30:     public FlowSignType SignType;
  31: }
很多的公用類都可以使用這樣的手法來達到繼承的目的。
