from django.http import HttpResponse
from django.shortcuts import render
from datetime import datetime

def home(request):
    return render(request,'news/accueil.html')
def propos_projet(request):
    return render(request, 'news/projet.html')
def team(request):
    return render(request,'news/MSN2.html')
def files(request):
    return render(request,'news/dwl.html')
