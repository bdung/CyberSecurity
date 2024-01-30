
using System.Collections.Generic;
using UnityEngine;

public class DialogString : MonoBehaviour
{
	// NPC police
	public List<List<string>> docBeginStartingGame = new List<List<string>>{
		new List<string>{"Xin chao, chao mung ban den voi game chung toi",
		"Hay den sow canh sat dde nhan nhiem vu nhe!"}
	};
	public List<List<string>> docBeginStartingSeePoliceDegree1 = new List<List<string>>{
		new List<string> {
		 "Trong thời buổi mà lừa đảo qua mạng ngày càng tràn lan và tinh vi, nhiều người dân đã vướng phải những phi vụ lừa đảo, dẫn đến mất mát về vật chất lẫn tinh thần.",
		 "Sở cảnh sát Thành phố A chúng tôi đang bị thiếu hụt nhân lực trầm trọng.",
		  "Chúng tôi đã nhận được rất nhiều hồ sơ ứng tuyển, và chúng tôi nhận thấy hồ sơ của bạn rất đặc biệt nên đã liên hệ với bạn để cùng bàn bạc về việc này. Chúng ta bắt đầu nhé?"
		 },
		 new List<string> {
		 "Vâng thưa quản lý.",
		 },
		 new List<string> {
		 "Công việc này là một công việc khó khăn, đòi hỏi về những kiến thức chuyên môn liên quan phải vững chắc.",
		 "Đây là một hành trình dài và không dễ dàng, bạn chắc chắn muốn tham gia cùng chúng tôi chứ ?",

		 },
		 new List<string> {
		 "Đúng vậy, tôi rất yêu thích về an toàn thông tin và muốn góp sức mình để giúp đỡ người dân, phòng ngừa các vụ lừa đảo qua mạng ngày càng tràn lan trong xã hội. ",
		 "Vậy nên tôi ở đây để ứng tuyển vào vị trí thực tập sinh."
		 },
		 new List<string> {
		 "Bạn hãy đến cấp 1 để hoàn thành nhiệm vụ đầu tiên",
		 "Chúc bạn may mắn"
		 },


	};
	public List<List<string>> docBeginStartingSeePoliceDegree2 = new List<List<string>>{
		new List<string> {
		 "Chúc mừng bạn đã hoàn thành cấp 1. Hãy đến cấp 2",
		},
		 new List<string> {
		 "Vâng thưa quản lý.",
		 },

	};
	public List<List<string>> docBeginStartingSeePoliceDegree3 = new List<List<string>>{
		new List<string> {
		 "Chúc mừng bạn đã hoàn thành cấp 2. Hãy đến cấp 3",
		},
		 new List<string> {
		 "Vâng thưa quản lý.",
		 },
	};
	public List<List<string>> docBeginStartingSeePoliceDegree4 = new List<List<string>>{
		new List<string> {
		 "Chúc mừng bạn đã hoàn thành cấp 3. Hãy đến cấp 4",
		},
		 new List<string> {
		 "Vâng thưa quản lý.",
		 },

	};

	public List<List<string>> docNoCompleteDegree1 = new List<List<string>>{
		new List<string> {"Chào Ban, chúng tôi nhận thấy bạn chưa hoàn thành tiến độ của Caap bac 1, hãy quay lại địa điểm Cap bac 1 và tiếp tục làm nhiệm vụ. ",
		"Chúc bạn may mắn."},

	};
	public List<List<string>> docNoCompleteDegree2 = new List<List<string>>{
		new List<string> {"Chào Ban, chúng tôi nhận thấy bạn chưa hoàn thành tiến độ của Caap bac 2, hãy quay lại địa điểm Cap bac 1 và tiếp tục làm nhiệm vụ. ",
		"Chúc bạn may mắn."},

	};
	public List<List<string>> docNoCompleteDegree3 = new List<List<string>>{
		new List<string> {"Chào Ban, chúng tôi nhận thấy bạn chưa hoàn thành tiến độ của Caap bac 3, hãy quay lại địa điểm Cap bac 1 và tiếp tục làm nhiệm vụ. ",
		"Chúc bạn may mắn."},

	};
	public List<List<string>> docNoCompleteDegree4 = new List<List<string>>{
		new List<string> {"Chào Ban, chúng tôi nhận thấy bạn chưa hoàn thành tiến độ của Caap bac 4, hãy quay lại địa điểm Cap bac 1 và tiếp tục làm nhiệm vụ. ",
		"Chúc bạn may mắn."},

	};

	// NPC degree 1
	public List<List<string>> docBeginStartingSeeNPCDegree1 = new List<List<string>>{
		new List<string> {"Chào <Tên người chơi>, Chao mung bạn đến trò chơi cấp 1"},
	};
	public List<List<string>> docAfterSeeNPCDegree1 = new List<List<string>>{
		new List<string> {"Tiếp tục trò chơi cấp 1"},
	};
	public List<List<string>> docAfterCompleteDegree1 = new List<List<string>>{
		new List<string> {"Bạn đã hoàn thành cấp 1. bạn có muốn Tiếp tục trò chơi cấp 1 nữa không"},
	};
	// NPC degree 2
	public List<List<string>> docBeginStartingSeeNPCDegree2 = new List<List<string>>{
		new List<string> {"Chào <Tên người chơi>, Chao mung bạn đến trò chơi cấp 2"},
	};
	public List<List<string>> docAfterSeeNPCDegree2 = new List<List<string>>{
		new List<string> {"Tiếp tục trò chơi cấp 2"},
	};
	public List<List<string>> docAfterCompleteDegree2 = new List<List<string>>{
		new List<string> {"Bạn đã hoàn thành cấp 2. bạn có muốn Tiếp tục trò chơi cấp 2 nữa không"},
	};

	// degree 3
	public List<List<string>> docBeginStartingSeeNPCDegree3 = new List<List<string>>{
		new List<string> {"Chào <Tên người chơi>, Chao mung bạn đến trò chơi cấp 3"},
	};
	public List<List<string>> docAfterSeeNPCDegree3 = new List<List<string>>{
		new List<string> {"Tiếp tục trò chơi cấp 3"},
	};
	public List<List<string>> docAfterCompleteDegree3 = new List<List<string>>{
		new List<string> {"Bạn đã hoàn thành cấp 3. bạn có muốn Tiếp tục trò chơi cấp 3 nữa không"},
	};
	// degree 3
	public List<List<string>> docBeginStartingSeeNPCDegree4 = new List<List<string>>{
		new List<string> {"Chào <Tên người chơi>, Chao mung bạn đến trò chơi cấp 4"},
	};
	public List<List<string>> docAfterSeeNPCDegree4 = new List<List<string>>{
		new List<string> {"Tiếp tục trò chơi cấp 4"},
	};
	public List<List<string>> docAfterCompleteDegree4 = new List<List<string>>{
		new List<string> {"Bạn đã hoàn thành cấp 4. bạn có muốn Tiếp tục trò chơi cấp 4 nữa không"},
	};
	void Start()
	{

	}



}