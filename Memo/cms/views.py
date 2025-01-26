# from django.shortcuts import render

# # Create your views here.
# # coding: utf-8

# #import django_filters
# from rest_framework import viewsets#, filters

# from .models import User, Memo
# from .serializer import UserSerializer, MemoSerializer
# from django.views.generic import TemplateView
# from django.shortcuts import render
# from django.http import JsonResponse
# from rest_framework.views import APIView
# from rest_framework.response import Response


# class UserViewSet(viewsets.ModelViewSet):
#     queryset = User.objects.all()
#     serializer_class = UserSerializer


# class MemoViewSet(viewsets.ModelViewSet):
#     queryset = Memo.objects.all()
#     serializer_class = MemoSerializer


# class MemosView(APIView):
#     template_name = ""

#     def get(self, request):
#         data = {'message': 'Hello from Django!'}
#         return Response(data)

    
#     def post(self,request):
#         self.kwargs["message"] = "ボタンを押したらこれがPOSTです"
#         return render(request, 'http://127.0.0.1:8000/api/',context = self.kwargs)

from django.http import JsonResponse
from django.views.decorators.csrf import csrf_exempt
import json
#from cms.models import Memo
#from cms.models import User
#from datetime import date, datetime


@csrf_exempt  # CSRF保護を無効化（開発時のみ）
def game_data(request):
    if request.method == 'GET':
        # GETリクエストの処理
        
         #memos = Memo.objects.all()
         #memos_list = list(memos.values())
         #json_List = json.dumps(memos_list,default=str,ensure_ascii=False)
         #print(json_List)
         #return JsonResponse(json_List,safe=False)
         
        return JsonResponse({
            'message': 'GET request received',
            'data': {
                'id': 1,
                'name': 'test',
                'value': 123
            }
        })
        
                

    elif request.method == 'POST':
        # POSTリクエストの処理
        try:
            data = json.loads(request.body)
            return JsonResponse({
                'message': 'POST request received',
                'data': {
                    'id': 1,
                    'name': data.get('name'),
                    'value': data.get('value')
                }
            })
        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON'}, status=400)
