# Minesweeper
   [![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2FTakeNewcare&count_bg=%23939DAE&title_bg=%2361ACCD&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)
   
<br>

<p align="center">
   
  <img src ="../main/Image/start.png"  width="250" height="300" align='left'>
  <img src ="../main/Image/win.png"  width="250" height="300">
</p>


## 프로젝트 소개
저는 부산폴리텍 하이테크 과정에서 c#과 winform 등을 배우는 중 입니다.<br>
저의 첫 프로젝트는 지뢰찾기 게임으로 아래의 싸이트를 참고하여 만들어 보았습니다.<br>
<br>
참고 싸이트 : https://minesweeper.online/ko/
<br><br>
Reason for making : studying c# and winform <br>
Busan Polytechnic High-Tech Course <br>
Development period : 5 days <br>
<br>

## 개발팀 소개
<table>
  <tr>
    <th>정진영</th>
    <td  rowspan="3">
    안녕하십니까!, 물류팀에서 일을 하다 IT 부서 분들과 친해져 해당 분야를 알게 되었고,
    이번 high-tech 과정을 통해 새로운 길을 향해 성장하고 있습니다.
   <br>
   <br>
    처음 접하는 분야라 두려움이 있었지만,<br> 
    오류가 났을 때 해결해야 직성이 풀리는 저의 성격과 잘 맞아 꾸준하게 성장하고 있습니다. <br> 
   감사합니다.
    </td>
  </tr>
  <tr>
    <td> <img src ="../main/Image/me.JPG"  width="200" height="200"></td>
  </tr>
  <tr>
    <td align='center'>wlsdud1525@naver.com</td>
  </tr>
</table>
<br>
<hr> 

## Stacks
### Environment
<img src="https://img.shields.io/badge/visualstudio-5C2D91?style=flat-square&logo=visualstudio&logoColor=white"/> <img src="https://img.shields.io/badge/github-181717?style=flat-square&logo=github&logoColor=white"/>

### Development
<img src="https://img.shields.io/badge/.NET-512BD4?style=flat-square&logo=.NET&logoColor=white"/> <img src="https://img.shields.io/badge/csharp-512BD4?style=flat-square&logo=csharp&logoColor=white"/> 

### 화면 구성
|Start|Win|End|
|:---:|:---:|:---:|
|<img src ="../main/Image/start.png"  width="250" height="300">|<img src ="../main/Image/win.png"  width="250" height="300">|<img src ="../main/Image/end.png"  width="250" height="300">|
|**10x10**|**15x15**|**20x20**|
|<img src ="../main/Image/10x10.png"  width="250" height="300">|<img src ="../main/Image/15x15.png"  width="250" height="300">|<img src ="../main/Image/20x20.png"  width="250" height="300">|


## main function
1. 안전지대 클릭 시 주변의 8칸 오픈(안전지대 열리면 재귀)
2. 플래그 기능
3. 경계선 클릭 시 범위 보여주는 기능

## 새로 알게된 점과 느낀점
이번 프로젝트를 진행하면서 새로 알게된 부분은 label 컨트롤은 다른 컨트롤들과는 다르게 상속받아서 사용할 수 없다는 부분입니다.
<br><br>
지뢰를 만들 때 button 컨트롤을 상속 받는 클래스를 만들어 form에서 반복문을 통해 최대갯수로 인스턴스를 생성하여 배열에 담고  visible 속성을 통해 레벨에 따라 필요한 부분만 보이게 만들었지만,
<br><br>
전광판 객체를 생성할 때 label 컨트롤 상속을 시도하다 알게된 사실이며 label 뿐만 아니라, ListView와 ToolStrip 등 기본 컨트롤 중 몇가지 컨트롤이 상속할 수 없다는 점을 알게 되었습니다.
<br>
