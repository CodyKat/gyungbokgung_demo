# git convention

main - 빌드가능한 배포버전 <br/>
dev - 개발중인 main 브랜치

1. dev 브랜치로부터 기능별로 브랜치를 파서 개발하도록 함
2. 브랜치명은 브랜치목적 + / + 브랜치역할로 작성함
   ex) feat/create_map     fix/player_move
3. commit 내용은 한국어||영어로 작성하고 한줄로 간단히 작성함
4. dev 브랜치로의 pull request를 통해 merge함.
5. merge가 끝난 브랜치는 merge를 한 사람이 닫음
6. develop_log 폴더안에 각자 개발일지를 작성하도록 함
