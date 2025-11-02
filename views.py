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

# -*- coding: utf-8 -*-
from django.http import JsonResponse
from django.views.decorators.csrf import csrf_exempt
import json
import requests
from cms.models import Memo
from cms.models import Users
from cms.models import Query
from django.core.exceptions import ValidationError
from django.http import HttpResponse
from django.contrib.auth import authenticate, login , logout
#from . import forms
import datetime
from django.forms.models import model_to_dict
from typing import Dict


loginUser = None

# Dify APIの認証キー
API_KEY = 'app-i1ZFeiksPAKFLJqlC9IJbaMM'  # 取得したAPIキーに置き換えてください
# Dify APIのベースURL
BASE_URL = 'https://api.dify.ai/v1/chat-messages'

def custom_decode(obj: dict):
    if isinstance(obj, dict):
        obj = Users(username=obj["username"], password = obj["password"],email = obj["email"])
        return obj
    else:
        return obj

def login_decode(obj: dict):
    if isinstance(obj, dict):
        user = authenticate(username=obj["username"], password = obj["password"])
        return user
    else:
        return None

def memo_decode(obj: dict):
    if isinstance(obj, dict):
        obj = Memo(name=obj["name"], content = obj["content"], author = Users.objects.get(id = obj["author"]), date = datetime.datetime.now())
        return obj
    else:
        return obj


def memoOverWrite_decode(obj: dict):
    if isinstance(obj, dict):
        obj = Memo(id=obj["id"], name=obj["name"], content = obj["content"], date = datetime.datetime.now())
        return obj
    else:
        return obj

def userOverWrite_decode(obj: dict):
    if isinstance(obj, dict):
        obj = Users(id=obj["id"], username=obj["username"], password = obj["password"])
        return obj
    else:
        return obj

def aiChat_decode(obj: dict):
    if isinstance(obj, dict):
        obj = Query(user=obj["user"], chat=obj["chat"])
        return obj
    else:
        return obj




@csrf_exempt  # CSRF保護を無効化（開発時のみ）
def game_data(request):
    if request.method == 'GET':
        # GETリクエストの処理
         #memos = Memo.objects.all()
         memos = Memo.objects.filter(author=loginUser.id)
         memos_list = list(memos.values())
         json_List = json.dumps(memos_list,default=str,ensure_ascii=False)
         return JsonResponse(json_List,safe=False, json_dumps_params={'ensure_ascii': False})


    elif request.method == 'POST':
        # POSTリクエストの処理
        try:
            data = json.loads(request.body, object_hook=memo_decode)
            if data is not None:
                try:
                    data.save()
                    #return redirect('accounts:home')
                    #return JsonResponse(return_data,safe=False, json_dumps_params={'ensure_ascii': False})
                    return HttpResponse("MemoRegistered.")
                except ValidationError as e:
                    data.add_error('memoPassword', e)
            # return render(request,'accounts/signin.html', context={
            #     'signin_form': signin_form,
            # })
                    #return JsonResponse(return_data,safe=False, json_dumps_params={'ensure_ascii': False})#({
                    return HttpResponse("MemoRegistered.")

        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON'}, status=400)


@csrf_exempt
def users_data(request):
    global loginUser

    if request.method == 'GET':
         #global loginUser
         user = loginUser
         #user_list = list(user)
         #json_List = json.dumps(user_list,default=str,ensure_ascii=False)
         if user is not None:
             json_List = json.dumps(model_to_dict(user), default=str)
             return JsonResponse(json_List,safe=False, json_dumps_params={'ensure_ascii': False})
          #return HttpResponse(str(user.username))
    elif request.method == 'POST':
        # POSTリクエストの処理
        try:
            user = json.loads(request.body, object_hook=custom_decode)
            if user is not None:
                try:
                    user.set_password(user.password)
                    user.save()
                    loginUser = user
                    json_List = json.dumps(model_to_dict(user), default=str)
                    return JsonResponse(json_List,safe=False, json_dumps_params={'ensure_ascii': False})
                    #return HttpResponse("Registered.")
                except ValidationError as e:
                    user.add_error('password', e)
                    return HttpResponse("Not Register.")

        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON'}, status=400)

@csrf_exempt
def users_login(request):
    if request.method == 'GET':
        return HttpResponse("Hellogin")
    elif request.method == 'POST':
        try:
            user = json.loads(request.body, object_hook=login_decode)
            if user is not None:
                try:
                    login(request,user)
                    global loginUser
                    loginUser = user
                    json_List = json.dumps(model_to_dict(user), default=str)
                    return JsonResponse(json_List,safe=False, json_dumps_params={'ensure_ascii': False})
                    #return HttpResponse("Login.")
                except ValidationError as e:
                    user.add_error('password', e)
                    return HttpResponse("LoginError.", status=500)
            else:
                return HttpResponse("user is None", status=400)

        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON'}, status=400)

@csrf_exempt
def users_logout(request):
    logout(request)
    global loginUser
    loginUser = None
    return HttpResponse("Logout")




@csrf_exempt
def memo_overwrite(request):
    if request.method == 'GET':
        return HttpResponse("OVERWRITE")


    elif request.method == 'POST':
        # POSTリクエストの処理
        try:
            data = json.loads(request.body, object_hook=memoOverWrite_decode)
            if data is not None:
                    try:
                        #instance =  Memo.objects.get(data.id)
                        #instance.name = data.name
                        #instance.content = data.content
                        #instance.date = data.date
                        #instance.save()
                        obj, created = Memo.objects.update_or_create(
                            id=data.id,
                            defaults={'name': data.name, 'content': data.content, 'date': data.date}
                        )
                        if created == False:
                           return HttpResponse("MemoOverWrited.")
                        else:
                           return HttpResponse("MemoCreated.")
                    except:
                        return HttpResponse("MemoOverWriteError")

        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON'}, status=400)




@csrf_exempt
def memo_delete(request):
    if request.method == 'GET':
        return HttpResponse("DELETE")


    elif request.method == 'POST':
        # POSTリクエストの処理
        try:
            # data = json.loads(request.body, object_hook=memoOverWrite_decode)
            # 削除にはidがあればいいので、hookは使わずシンプルにidだけ取得
            data = json.loads(request.body)
            memo_id = data.get("id")

            # if data is not None:
            # 判定はするのは idがあるかだけ
            if memo_id is not None:
                    try:
                        # instance =  Memo.objects.get(data.id)
                        # id の指定
                        instance = Memo.objects.get(id=memo_id)
                        instance.delete()
                        return HttpResponse("MemoDeleted.")
                    except:
                        return HttpResponse("MemoDeleteError")


        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON'}, status=400)


@csrf_exempt
def user_overwrite(request):
    if request.method == 'GET':
        return HttpResponse("USEROVERWRITE")


    elif request.method == 'POST':
        # POSTリクエストの処理
        try:
            data = json.loads(request.body, object_hook=userOverWrite_decode)
            if data is not None:
                    try:
                        obj, created = Users.objects.update_or_create(
                            id=data.id,
                            defaults={'username': data.username}
                        )
                        if created == False:
                            global loginUser
                            obj.set_password(data.password)
                            obj.save()
                            loginUser = obj
                            json_List = json.dumps(model_to_dict(obj), default=str)
                            return JsonResponse(json_List,safe=False, json_dumps_params={'ensure_ascii': False})
                        else:
                           return HttpResponse("UserCreated.")
                    except:
                        return HttpResponse("UserOverWriteError")


        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON'}, status=400)




@csrf_exempt
def user_delete(request):
    if request.method == 'GET':
        return HttpResponse("USERDELETE")


    elif request.method == 'POST':
        # POSTリクエストの処理
        try:
            # data = json.loads(request.body, object_hook=memoOverWrite_decode)
            # 削除にはidがあればいいので、hookは使わずシンプルにidだけ取得
            data = json.loads(request.body)
            user_id = data.get("id")

            # if data is not None:
            # 判定はするのは idがあるかだけ
            if user_id is not None:
                    try:
                        # instance =  Memo.objects.get(data.id)
                        # id の指定
                        global loginUser
                        instance = Users.objects.get(id=user_id)
                        instance.delete()
                        loginUser = None
                        return HttpResponse("UserDeleted.")
                    except:
                        return HttpResponse("UserDeleteError")


        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON'}, status=400)



@csrf_exempt
def aiChat(request):
    if request.method == 'GET':
        return HttpResponse("AIChat")


    elif request.method == 'POST':
        # POSTリクエストの処理
        try:
            ques = json.loads(request.body, object_hook=aiChat_decode)
            if ques is not None:
                    #try:
                            """
                            Dify APIにリクエストを送信し、応答を取得する関数

                            :param query: ユーザーの質問
                            :param user: ユーザー識別子
                            :return: APIからの応答テキスト
                            """
                            headers = {
                                'Authorization': f'Bearer {API_KEY}',
                                'Content-Type': 'application/json'
                            }

                            data: Dict[str, any] = {
                                "inputs": {},
                                "query": ques.chat,
                                "response_mode": "blocking",
                                "user": ques.user,
                            }

                            response = requests.post(BASE_URL, headers=headers, json=data)
                            response.raise_for_status()

                            answer = response.json()['answer']
                            print(answer)
                            return JsonResponse(answer,safe=False, json_dumps_params={'ensure_ascii': False})
                   # except:
                        #return HttpResponse("AIError")


        except json.JSONDecodeError:
            return JsonResponse({'error': 'Invalid JSON'}, status=400)
