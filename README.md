<h1 align="center">Sweet Dream 甜美的梦</h1>

 
**o.O队：刘健泓 刘仲衡 辛启轩**
## 一、游戏介绍

### 游戏背景
- 本作是一款2d平台跳跃游戏，玩家会扮演梦境精灵，深入探索梦境主人的梦境，在6个关卡中，玩家将会看到梦境主人的经历、内心渴望，代表主人不同的情绪。随着探索的深入，玩家会进入梦境主人的深层梦境，遇见更强大的敌人和怪物，最终进入主人梦境的最深处，打败他内心的阴影，拯救陷入焦虑不安中的主人。
本作的梦境是一个大学生的梦境，游戏将展示他所遇到的困难，包括内卷问题，学习压力，交友问题，未来方向迷惘。	

### 创意来源
- 弗洛伊德认为梦境是潜意识欲望和冲突的表达，在他的作品《梦的解析》中提出，梦境是心灵深处欲望的满足，是通过象征性语言表达的，这种象征性语言可以被解析来揭示潜在的心理状态和冲突。
本作的灵感来源于大学生内卷现象与心理问题，例如在校园墙可以看到大量的关于成绩、交友、内卷与人生未来方向的问题，说明大学生的相关内卷现象与心理问题变得越发普遍。
因此 我们希望在作品的游戏上和叙事上能结合现实问题，使玩家在游玩的同时也能引起思考。因此我们在游戏内容中也加入了各种叙事与对白，深入地探讨了大学生面临的心理问题，如内卷、学习压力、交友困难和对未来的迷茫。
于是本作以“梦境”与“大学生心理问题”为背景，讲述一个梦境精灵拯救梦境主人大学生的故事，梦境精灵的设定象征着一种希望和拯救，表明梦境主人内心渴望被拯救，而阴影怪物象征着主人内心的不安和恐惧，玩家需要打败内心的阴影，拯救梦境主人。

### 游戏目标
- 透过跑酷和解谜深入主人的梦境，打败其梦境的阴影，最终拯救主人。

### 游戏时长
- 预计20-30分钟

### 核心理念：希望作品不仅仅让玩家觉得“好玩”，还要引发玩家“对现实的思考”
## 二、实现功能
### 游戏功能以玩家游戏体验为中心设计，致力于在操作、打击、体验上给玩家一个沉浸、真实的游戏体验

- **运动系统：** 玩家的移动、跳跃、冲刺等动作，包括按键时间与跳跃高度、土狼时间、跳跃缓冲运动手感优化。
- **战斗系统：** 玩家的攻击、远程攻击、受伤、回复、冷却CD、攻击与受击反馈、顿帧、怪物的AI等。
- **镜头抖动：** 镜头的抖动，用在战斗与交互功能上，增加反馈感受。
- **运镜系统：** 包括镜头偏移，镜头跟随等。
- **粒子特效系统：** 受击粒子、场景粒子、运动粒子、交互粒子等，增加玩家的打击体验。
- **光影与后处理系统：** 物品，玩家，场景的光影颜色、强度等的控制、泛光效果、画面扭曲效果。
- **音效系统：** 背景音乐与音效的控制，增加玩家的打击感与游戏的沉浸体验。
- **对话系统：** 对话的触发与对话流程，表现游戏的剧情。
- **UI系统：** 开始菜单，暂停菜单，设定菜单、音量修改、信息查阅等。
- **转场系统：** 转场动画与转场功能的实现，增加游戏的沉浸体验。
- **JSON存储系统：** 关卡的存储、玩家的进度等信息的存储与读取。
- **交互系统：** 对于对象的交互，会有相应的提示与反馈，增加玩家的交互体验。
- **成就系统：** 根据玩家的游玩进度给与成就，提升玩家的成就感。
- **Boss：** 第一阶段战斗，打败后进入第二阶段。
- **游戏机关：** 超过20个解谜与跑酷机关。

## 三、设计目的
### 为给玩家一个沉浸的游戏体验，游戏中100%美术设计为团队原创
### 关卡设计
- 本作的关卡设计主要使用了紫色、蓝色和红色三种颜色来代表梦境主人不同的情绪：深紫色给人一种恐惧和忧愁的感觉，蓝色能够让人感到放松，而深红色则能让人愤怒和压抑。每个关卡的设计都各不相同，深紫色关卡主要以跑酷元素为主，通过机关让玩家产生紧张感；蓝色关卡因其颜色具有放松的视觉效果，整体以朦胧效果呈现；深红色关卡则以压抑的视觉效果为设计目的，通过图案的密集排列呈现。
<img src="Art work/PurpleScene.png" width="auto" height="300px">
<img src="Art work/BlueScene.png" width="auto" height="300px">
<img src="Art work/RedScene.png" width="auto" height="300px">

### 人物设计
- 以精灵为题，用拟人化的方式画出主体，人物色调以蓝色为主，突显出人物的速度感。

<img src="Art work/Player un/Player_Un-1.png" width=auto height="300px">

### 怪物设计
- 小怪及boss用了深红及深紫，与关卡颜色的作用一样，两只小怪以虫及蝙蝠为形象，而boss则先拟人化以盔甲的状态出现，第二阶段就变为类液态的型像出现，与第一阶段作出被拘束及解开束缚的对比。
<div>
  <img src="Art work/小怪2/怪2-1.png" width="auto" height="200px">
  <img src="Art work/小怪1/怪1-1.png" width="auto" height="200px">
</div>
<img src="Art work/Boss_attack_1/Boss_attack_1-11.png" width=auto height="300px">

## 四、使用说明

### 代码构建工具：

- **Unity Build功能** 使用 Unity 的 Build 功能打包代码。

### 第三方库：

- **DOTween：** 一个用于 Unity 的动画库，用于创建流畅的动画效果。

### Unity版本：

- **Unity版本：** 2022.3.20f1

### 操作系统版本：

- **操作系统版本：** Windows 10 或以上版本

### 打开方法：

1. 下载并解压“Sweet Dream打包文件”。
2. 在解压后的文件夹中找到“Sweet_Dream.exe”。
3. 双击“Sweet_Dream.exe”以打开游戏。


### 操作方法：

1. **左右移动：** 使用键盘上的 A 和 D 键。
2. **镜头移动：** 使用键盘上的 W 和 S 键。
3. **跳跃：** 按下空格键（根据按键时间增加跳跃高度）。
4. **冲刺：** 按下 Shift 键。
5. **物件交互：** 按下 E 键。
6. **近程攻击：** 使用鼠标左键。
7. **远程攻击：** 使用鼠标右键。
8. **血量回复：** 按下 F 键。
9. **菜单：** 按下 ESC 键。
10. **声音控制：** 在菜单的 Music 界面进行设置。



## 五、分工
- **辛启轩：** 游戏美术，角色设计，UI设计，场景设计。
- **刘仲衡：** 系统策划，程序设计，战斗系统设计，战斗场景制作。
- **刘健泓：** 游戏策划，程序设计，关卡设计，跑酷场景制作。

## 六、所用技术与工具
- Unity, C#, Visual Studio, Photoshop, Procreate, GitHub多人协作。

