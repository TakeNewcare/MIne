# <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=duckdb&logoColor=red"/> Minesweeper
   [![Hits](https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2FTakeNewcare&count_bg=%23939DAE&title_bg=%2361ACCD&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false)](https://hits.seeyoufarm.com)
   
<br>

<p align="center">
   
  <img src ="../main/Image/start.png"  width="250" height="300" align='left'>
  <img src ="../main/Image/win.png"  width="250" height="300">
</p>





## <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=googledocs&logoColor=black"/> Project Info
저는 부산폴리텍 하이테크 과정에서 c#과 winform에 대해 배우는 중 매번 컨텐츠의 끝을 보지 않는 부분이 아쉬워
혼자 끝까지 만들어 본 미니 프로젝트입니다.
마인 숫자(플래그 갯수)와 경과 시간 그리고 재시작 버튼을 담고 있는 패널을 객체를 만들어 추가하였습니다.
또한, 게임 시작 시 사용자로부터 레벨을 입력 받기 위한 form을 추가하였습니다.

Reason for making: studying c# and winform <br>
Busan Polytechnic High-Tech Course <br>
Development period: 3 days <br>
<br>

## 개발팀 소개
<table>
  <tr>
    <th>정진영</th>
    <td  rowspan="3">

      내용적기  
    
    
    </td>
  </tr>
  <tr>
    <td> <img src ="../main/Image/me.JPG"  width="200" height="200"></td>
  </tr>
  <tr>
    <td align='center'>wlsdud1525@naver.com</td>
  </tr>
</table>
 

## Stacks
### Environment
<img src="https://img.shields.io/badge/visualstudio-5C2D91?style=flat-square&logo=visualstudio&logoColor=white"/> <img src="https://img.shields.io/badge/github-181717?style=flat-square&logo=github&logoColor=white"/>

### Development
<img src="https://img.shields.io/badge/.NET-512BD4?style=flat-square&logo=.NET&logoColor=white"/> <img src="https://img.shields.io/badge/csharp-512BD4?style=flat-square&logo=csharp&logoColor=white"/> 

### Communication
<img src="https://img.shields.io/badge/slack-4A154B?style=flat-square&logo=slack&logoColor=white"/>

### <img src="https://img.shields.io/badge/-FFFFFF?style=flat-square&logo=airplayvideo&logoColor=black"/>Screen configuration
|Start|Win|End|
|:---:|:---:|:---:|
|<img src ="../main/Image/start.png"  width="250" height="300">|<img src ="../main/Image/win.png"  width="250" height="300">|<img src ="../main/Image/end.png"  width="250" height="300">|
|**10x10**|**15x15**|**20x20**|
|<img src ="../main/Image/10x10.png"  width="250" height="300">|<img src ="../main/Image/15x15.png"  width="250" height="300">|<img src ="../main/Image/20x20.png"  width="250" height="300">|

## 

## main function
### 1. 안전지대 클릭 시 주변 오픈 기능
```
          // 클릭한 셀이 안전지대(.)이면 열고, 열린 지역도 안전지대이면 재귀함수로 한번 더 오픈
        public void OpenSurround(int x, int y)
        {
              // 클릭한 셀이 최소 / 최대 셀일 시 주변 8칸을 찾을 때 셀의 최소 인덱스, 최대 인덱스를 넘지 않게 찾기 위한 max, min 함수
              // 클릭한 셀이 0일 경우 0-1을 하면 -1이고 -1이란 셀이 없기 때문에 -1과 0 둘 중 최댓값 선택하기
            for (int i = Math.Max(x - 1, 0); i <= Math.Min(x + 1, current_row - 1); i++)
            {
                for (int j = Math.Max(y - 1, 0); j <= Math.Min(y + 1, current_col - 1); j++)
                {
                    if (cells[i, j].Text != "")   // 이미 오픈된 박스이면 패스
                        continue;

                    OpenCell(cells[i, j]);


                    if (cells[i, j].Text == ".")   // 만약 새로 오픈한 곳이 안전지대이면 해당 위치 기준으로 재귀함수 사용
                        OpenSurround(i, j);
                }
            }
        }

          // 경계라인 클릭 시 주변 범위 보여주기
        private void Surround_Sign(int x, int y)
        {
            for (int i = Math.Max(x - 1, 0); i <= Math.Min(x + 1, current_row); i++)
            {
                for (int j = Math.Max(y - 1, 0); j <= Math.Min(y + 1, current_col); j++)
                {
                    if (cells[i, j].Text != "")
                        continue;

                    cells[i, j].BackColor = Color.FromArgb(0, 153, 153);

                }
            }
        }
```

### 2. 플래그 기능
```
          // 우클릭 시 깃발, 경계라인 클릭 시 범위를 나타내는 동작
        private void Flag_btn(object sender, MouseEventArgs e)
        {

            Cell c = (Cell)sender;  // 이벤트 발생한 객체 찾기


              // 경계라인 클릭 시 범위 알려주는 기능 => 클릭 이벤트에 걸면 작동 x
            if (c.Text != "" && c.Text != "." && c.Text != "P")
                Surround_Sign(c.x, c.y);
            else
            {
                if (e.Button != MouseButtons.Right)
                    return;

                if (game_start_true)   // 첫 클릭이 우클릭이라도 시작으로 간주하고 타이머 시작
                {
                    game_start_true = false; // 첫 클릭인 true 값을 false로 돌려 다음 클릭은 타이머 다시 시작하는거 막기
                    timer1.Start();
                }

                if (c.Text == "P")
                {
                    c.Text = "";
                    pan.L1.Text = (flag_count + 1).ToString();
                    flag_count += 1;
                }
                else if (c.Text == "")
                {
                    c.Text = "P";
                    pan.L1.Text = (flag_count - 1).ToString();
                    flag_count -= 1;
                    c.ForeColor = System.Drawing.Color.Red;
                }
            }

```
<p align='center' >
  <img src ="https://github.com/TakeNewcare/Project/assets/163362484/70c2bb76-74dd-4798-bc25-9e67e3b05f59"  width="250" height="300" >
</p>




<br>
참조
리드미 작성 방법 : https://velog.io/@luna7182/%EB%B0%B1%EC%97%94%EB%93%9C-%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8-README-%EC%93%B0%EB%8A%94-%EB%B2%95
