from django.http import HttpResponse
from django.shortcuts import render
from datetime import datetime
from .forms import SnippetForm

def home(request):
    return render(request,'news/accueil.html')
def propos_projet(request):
    return render(request, 'news/projet.html')
def team(request):
    return render(request,'news/MSN2.html')
def files(request):
    return render(request,'news/dwl.html')

def snippet_detail(request):
        if request.method == 'POST':
            form = SnippetForm(request.POST)
            if form.is_valid():
                form.save()
                return render(request,'news/success.html')

        form = SnippetForm()
        return render(request,'news/form.html', {'form': form})
